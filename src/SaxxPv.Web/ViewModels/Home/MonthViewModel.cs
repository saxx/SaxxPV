using Adliance.Buddy.DateTime;
using Microsoft.EntityFrameworkCore;
using SaxxPv.Web.Models.Database;
using SaxxPv.Web.Services;

namespace SaxxPv.Web.ViewModels.Home;

public class MonthViewModelFactory(Db db, PricingService pricingService)
{
    public async Task<MonthViewModel> Build(DateOnly day)
    {
        var min = day.ToDateTime(new TimeOnly(0, 0)).CetToUtc();
        var max = min.AddMonths(1);

        var rows = await db.Readings
            .AsNoTracking()
            .Where(x => x.DateTime >= min && x.DateTime < max)
            .ToListAsync();
        var result = new MonthViewModel(day)
        {
            SemsCounts = rows.Count
        };

        foreach (var r in rows)
        {
            var dayRow = result.Days.SingleOrDefault(x => x.DateTime.Date == r.DateTime.Date);
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
                dayRow.DateTime = r.DateTime;
                dayRow.Bought = r.DayBought;
                dayRow.Consumption = r.DayConsumption;
                dayRow.Sold = r.DaySold;
                dayRow.Price = -await pricingService.CalculateBuyPrice(new DateOnly(r.DateTime.Year, r.DateTime.Month, r.DateTime.Day), dayRow.Bought) +
                               await pricingService.CalculateSellPrice(new DateOnly(r.DateTime.Year, r.DateTime.Month, r.DateTime.Day), dayRow.Sold);
            }

            if (dayRow.BatterySocMin > r.CurrentBatterySoc) dayRow.BatterySocMin = r.CurrentBatterySoc;
            if (dayRow.BatterySocMax < r.CurrentBatterySoc) dayRow.BatterySocMax = r.CurrentBatterySoc;
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

    public int? SemsCounts { get; init; }
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
    }
}
