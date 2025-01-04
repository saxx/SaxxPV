using System.Globalization;
using Adliance.Buddy.DateTime;
using Microsoft.EntityFrameworkCore;
using SaxxPv.Web.Models.Database;
using SaxxPv.Web.Services;

namespace SaxxPv.Web.ViewModels.Home;

public class DayViewModelFactory(Db db, PricingService pricingService)
{
    public async Task<DayViewModel> Build(DateOnly day)
    {
        var min = day.ToDateTime(new TimeOnly(0, 0), DateTimeKind.Utc);
        var max = min.AddDays(1);

        var rows = await db.Readings
            .AsNoTracking()
            .Where(x => x.DateTime >= min && x.DateTime < max)
            .ToListAsync();
        var currentReading = rows.MaxBy(x => x.DateTime);
        if (currentReading == null) return new DayViewModel(day);

        await Task.CompletedTask;
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
            CurrentGridPrice = currentReading.CurrentGrid > 0
                ? await pricingService.CalculateSellPrice(day, currentReading.CurrentGrid / 1000d)
                : await pricingService.CalculateBuyPrice(day, currentReading.CurrentGrid / 1000d),
            DayBoughtPrice = await pricingService.CalculateBuyPrice(day, currentReading.DayBought),
            DaySoldPrice = await pricingService.CalculateSellPrice(day, currentReading.DaySold),
            ChartTimes = rows.Select(x => x.DateTime.ToString("HH:mm", CultureInfo.CurrentCulture)).ToList(),
            ChartPv = rows.Select(x => Math.Round(x.CurrentPv / 1000d, 1)).ToList(),
            ChartLoad = rows.Select(x => Math.Round(-x.CurrentLoad / 1000d, 1)).ToList(),
            ChartGrid = rows.Select(x => Math.Round(x.CurrentGrid / 1000d, 1)).ToList(),
            ChartBatterySoc = rows.Select(x => x.CurrentBatterySoc).ToList(),
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

    public double? DaySoldPrice { get; init; }
    public double? DayBoughtPrice { get; init; }

    public IList<string> ChartTimes { get; init; } = new List<string>();
    public IList<double> ChartPv { get; init; } = new List<double>();
    public IList<double> ChartLoad { get; init; } = new List<double>();
    public IList<double> ChartGrid { get; init; } = new List<double>();
    public IList<double> ChartBatterySoc { get; init; } = new List<double>();
}
