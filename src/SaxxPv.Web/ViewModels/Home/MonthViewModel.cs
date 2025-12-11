using Adliance.Buddy.DateTime;
using Microsoft.EntityFrameworkCore;
using SaxxPv.Web.Models.Database;
using SaxxPv.Web.Services;

namespace SaxxPv.Web.ViewModels.Home;

public class MonthViewModelFactory(Db db, PricingService pricingService)
{
    public async Task<MonthViewModel> Build(DateOnly day)
    {
        var min = new DateTime(day.Year, day.Month, 1, 0, 0, 0, DateTimeKind.Unspecified).CetToUtc();
        var max = new DateTime(day.Year, day.Month, 1, 0, 0, 0, DateTimeKind.Unspecified).AddMonths(1).CetToUtc();

        var rows = await db.Readings
            .AsNoTracking()
            .Where(x => x.DateTime >= min && x.DateTime < max)
            .ToListAsync();
        var result = new MonthViewModel(day)
        {
            DataPointsCount = rows.Count
        };

        foreach (var r in rows)
        {
            var dateTime = r.DateTime.UtcToCet();

            var dayRow = result.Days.SingleOrDefault(x => x.DateTime.Date == dateTime.Date);
            if (dayRow == null)
            {
                dayRow = new MonthViewModel.DayDetails
                {
                    BatterySocMin = double.MaxValue,
                    BatterySocMax = double.MinValue
                };
                result.Days.Add(dayRow);
            }

            if (dayRow.DateTime <= r.DateTime)
            {
                dayRow.DateTime = dateTime;
                dayRow.Bought = r.DayBought;
                dayRow.Consumption = r.DayConsumption;
                dayRow.Sold = r.DaySold;
                dayRow.Price = -await pricingService.CalculateBuyPrice(new DateOnly(r.DateTime.Year, r.DateTime.Month, r.DateTime.Day), dayRow.Bought) +
                               await pricingService.CalculateSellPrice(new DateOnly(r.DateTime.Year, r.DateTime.Month, r.DateTime.Day), dayRow.Sold);
                dayRow.BatteryCharge = r.DayBatteryCharge;
                dayRow.BatteryDischarge = r.DayBatteryDischarge;
                dayRow.TotalImport = r.TotalImport;
                dayRow.TotalExport = r.TotalExport;
            }

            if (dayRow.BatterySocMin > r.CurrentBatterySoc) dayRow.BatterySocMin = r.CurrentBatterySoc;
            if (dayRow.BatterySocMax < r.CurrentBatterySoc) dayRow.BatterySocMax = r.CurrentBatterySoc;

            dayRow.DataPoints++;
        }

        result.Days = result.Days.OrderBy(x => x.DateTime).ToList();
        return result;
    }
}

public class MonthViewModel(DateOnly day)
{
    public DateOnly Day { get; } = day;

    public bool IsCurrentMonth
    {
        get
        {
            var now = DateTime.UtcNow.UtcToCet();
            var today = new DateOnly(now.Year, now.Month, now.Day);
            return Day.Year == today.Year && Day.Month == today.Month;
        }
    }

    public int? DataPointsCount { get; init; }
    public IList<DayDetails> Days { get; set; } = new List<DayDetails>();

    public class DayDetails
    {
        public DateTime DateTime { get; set; }
        public double Consumption { get; set; }
        public double Sold { get; set; }
        public double Bought { get; set; }
        public double Price { get; set; }
        public double BatterySocMin { get; set; }
        public double BatterySocMax { get; set; }
        public int DataPoints { get; set; }
        public double? BatteryCharge { get; set; }
        public double? BatteryDischarge { get; set; }
        public double? TotalImport { get; set; }
        public double? TotalExport { get; set; }
    }
}
