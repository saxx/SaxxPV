using Adliance.Buddy.DateTime;
using Microsoft.EntityFrameworkCore;
using SaxxPv.Web.Models.Database;
using SaxxPv.Web.Services;

namespace SaxxPv.Web.ViewModels.Home;

public class DayViewModelFactory(Db db, PricingService pricingService)
{
    public async Task<DayViewModel> Build(DateOnly day)
    {
        var min = day.ToDateTime(new TimeOnly(0, 0)).CetToUtc();
        var max = min.AddDays(1);

        var rows = await db.Readings
            .AsNoTracking()
            .Where(x => x.DateTime >= min && x.DateTime < max)
            .ToListAsync();

        var currentReading = rows.MaxBy(x => x.DateTime);
        if (currentReading == null) return new DayViewModel(day);

        return new DayViewModel(day)
        {
            CurrentGrid = currentReading.CurrentGrid,
            CurrentLoad = currentReading.CurrentLoad,
            CurrentPv = currentReading.CurrentPv,
            CurrentBattery = currentReading.CurrentBattery,
            CurrentBatterySoc = currentReading.CurrentBatterySoc,
            DayBought = currentReading.DayBought,
            DayConsumption = currentReading.DayConsumption,
            DayPv = currentReading.DayTotal,
            DaySold = currentReading.DaySold,
            DayBatteryCharge =  currentReading.DayBatteryCharge,
            DayBatteryDischarge = currentReading.DayBatteryDischarge,
            CurrentGridPrice = currentReading.CurrentGrid > 0
                ? await pricingService.CalculateSellPrice(day, currentReading.CurrentGrid / 1000d)
                : await pricingService.CalculateBuyPrice(day, currentReading.CurrentGrid / 1000d),
            DayBoughtPrice = await pricingService.CalculateBuyPrice(day, currentReading.DayBought),
            DaySoldPrice = await pricingService.CalculateSellPrice(day, currentReading.DaySold),
            ChartPv = rows.ToDictionary(x => x.DateTime.UtcToCet(), x => Math.Round(x.CurrentPv / 1000d, 1, MidpointRounding.AwayFromZero)),
            ChartLoad = rows.ToDictionary(x => x.DateTime.UtcToCet(), x => Math.Round(-x.CurrentLoad / 1000d, 1, MidpointRounding.AwayFromZero)),
            ChartGrid = rows.ToDictionary(x => x.DateTime.UtcToCet(), x => Math.Round(x.CurrentGrid / 1000d, 1, MidpointRounding.AwayFromZero)),
            ChartBatterySoc = rows.ToDictionary(x => x.DateTime.UtcToCet(), x => x.CurrentBatterySoc),
            DaySelfUse = currentReading.DaySelfUse,
            SemsCounts = rows.Count,
            LastSemsDateTime = currentReading.DateTime
        };
    }
}

public class DayViewModel(DateOnly day)
{
    public DateOnly Day { get; } = day;

    public bool IsCurrentDay
    {
        get
        {
            var now = DateTime.UtcNow.UtcToCet();
            var today = new DateOnly(now.Year, now.Month, now.Day);
            return Day == today;
        }
    }

    public int? SemsCounts { get; init; }
    public DateTime? LastSemsDateTime { get; init; }

    public double? CurrentLoad { get; init; }
    public double? CurrentPv { get; init; }
    public double? CurrentGrid { get; init; }
    public double? CurrentBattery { get; init; }
    public double? CurrentBatterySoc { get; init; }
    public double? CurrentGridPrice { get; init; }

    public double? DayConsumption { get; init; }
    public double? DayPv { get; init; }
    public double? DaySelfUse { get; init; }
    public double? DayBought { get; init; }
    public double? DaySold { get; init; }

    public double? DayBatteryCharge { get; init; }
    public double? DayBatteryDischarge { get; init; }

    public double? DaySoldPrice { get; init; }
    public double? DayBoughtPrice { get; init; }

    public Dictionary<DateTime, double> ChartPv { get; init; } = new();
    public Dictionary<DateTime, double> ChartLoad { get; init; } = new();
    public Dictionary<DateTime, double> ChartGrid { get; init; } = new();
    public Dictionary<DateTime, double> ChartBatterySoc { get; init; } = new();
}
