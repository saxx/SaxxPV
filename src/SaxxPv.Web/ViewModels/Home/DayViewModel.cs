using System.Globalization;
using Adliance.Buddy.DateTime;
using SaxxPv.Web.Services;
using SaxxPv.Web.Services.Tables;

namespace SaxxPv.Web.ViewModels.Home;

public class DayViewModelFactory(ILogger<DayViewModelFactory> logger, TablesClient tableClient, PricingService pricingService)
{
    public async Task<DayViewModel> Build(DateOnly day)
    {
        var rows = tableClient.LoadSemsForDay(day, logger);
        var currentSemsRow = rows.MaxBy(x => x.DateTime);
        if (currentSemsRow == null) return new DayViewModel(day);

        await Task.CompletedTask;
        return new DayViewModel(day)
        {
            CurrentGrid = currentSemsRow.CurrentGrid,
            CurrentLoad = currentSemsRow.CurrentLoad,
            CurrentPv = currentSemsRow.CurrentPv,
            CurrentBattery = currentSemsRow.CurrentBattery,
            CurrentBatterySoc = currentSemsRow.CurrentBatterySoc,
            DayBought = currentSemsRow.DayBought,
            DayConsumption = currentSemsRow.DayConsumption,
            DayPv = currentSemsRow.DayTotal,
            DaySold = currentSemsRow.DaySold,
            CurrentGridPrice = currentSemsRow.CurrentGrid > 0
                ? pricingService.CalculateSellPrice(day, currentSemsRow.CurrentGrid / 1000d)
                : pricingService.CalculateBuyPrice(day, currentSemsRow.CurrentGrid / 1000d),
            DayBoughtPrice = pricingService.CalculateBuyPrice(day, currentSemsRow.DayBought),
            DaySoldPrice = pricingService.CalculateSellPrice(day, currentSemsRow.DaySold),
            ChartTimes = rows.Select(x => x.DateTime.ToString("HH:mm", CultureInfo.CurrentCulture)).ToList(),
            ChartPv = rows.Select(x => Math.Round(x.CurrentPv / 1000d, 1)).ToList(),
            ChartLoad = rows.Select(x => Math.Round(-x.CurrentLoad / 1000d, 1)).ToList(),
            ChartGrid = rows.Select(x => Math.Round(x.CurrentGrid / 1000d, 1)).ToList(),
            ChartBatterySoc = rows.Select(x => x.CurrentBatterySoc).ToList(),
            DaySelfUse = currentSemsRow.DaySelfUse,
            SemsCounts = rows.Count,
            LastSemsDateTime = currentSemsRow.DateTime
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

    public int? SemsCounts { get; set; }
    public DateTime? LastSemsDateTime { get; set; }

    public double? CurrentLoad { get; set; }
    public double? CurrentPv { get; set; }
    public double? CurrentGrid { get; set; }
    public double? CurrentBattery { get; set; }
    public double? CurrentBatterySoc { get; set; }
    public double? CurrentGridPrice { get; set; }

    public double? DayConsumption { get; set; }
    public double? DayPv { get; set; }
    public double? DaySelfUse { get; set; }
    public double? DayBought { get; set; }
    public double? DaySold { get; set; }

    public double? DaySoldPrice { get; set; }
    public double? DayBoughtPrice { get; set; }

    public IList<string> ChartTimes { get; set; } = new List<string>();
    public IList<double> ChartPv { get; set; } = new List<double>();
    public IList<double> ChartLoad { get; set; } = new List<double>();
    public IList<double> ChartGrid { get; set; } = new List<double>();
    public IList<double> ChartBatterySoc { get; set; } = new List<double>();
}
