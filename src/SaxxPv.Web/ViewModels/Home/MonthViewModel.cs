using Adliance.Buddy.DateTime;
using SaxxPv.Web.Services;
using SaxxPv.Web.Services.Tables;

namespace SaxxPv.Web.ViewModels.Home;

public class MonthViewModelFactory
{
    private readonly ILogger<DayViewModelFactory> _logger;
    private readonly TablesClient _tableClient;
    private readonly PricingService _pricingService;

    public MonthViewModelFactory(ILogger<DayViewModelFactory> logger, TablesClient tableClient, PricingService pricingService)
    {
        _logger = logger;
        _tableClient = tableClient;
        _pricingService = pricingService;
    }

    public async Task<MonthViewModel> Build(DateOnly day)
    {
        var rows = _tableClient.LoadSemsForMonth(day, _logger);

        await Task.CompletedTask;
        var result = new MonthViewModel(day)
        {
            SemsCounts = rows.Count,
        };

        foreach (var r in rows)
        {
            var dayRow = result.Days.SingleOrDefault(x => x.DateTime.Date == r.DateTime.Date);
            if (dayRow == null)
            {
                dayRow = new MonthViewModel.DayDetails();
                result.Days.Add(dayRow);
            }

            if (dayRow.DateTime <= r.DateTime)
            {
                dayRow.DateTime = r.DateTime;
                dayRow.Bought = r.DayBought;
                dayRow.Consumption = r.DayConsumption;
                dayRow.Sold = r.DaySold;
                dayRow.Price = -_pricingService.CalculateBuyPrice(new DateOnly(r.DateTime.Year, r.DateTime.Month, r.DateTime.Day), dayRow.Bought) +
                               _pricingService.CalculateSellPrice(new DateOnly(r.DateTime.Year, r.DateTime.Month, r.DateTime.Day), dayRow.Sold);
            }
        }

        result.Days = result.Days.OrderBy(x => x.DateTime).ToList();
        return result;
    }
}

public class MonthViewModel
{
    public MonthViewModel(DateOnly day)
    {
        Day = day;
    }

    public DateOnly Day { get; }

    public bool IsCurrentMonth
    {
        get
        {
            var now = DateTime.UtcNow.UtcToCet();
            var today = new DateOnly(now.Year, now.Month, now.Day);
            return Day.Year == today.Year && Day.Month == today.Month;
        }
    }

    public int? SemsCounts { get; set; }
    public IList<DayDetails> Days { get; set; } = new List<DayDetails>();

    public class DayDetails
    {
        public DateTime DateTime { get; set; }
        public double Consumption { get; set; }
        public double Sold { get; set; }
        public double Bought { get; set; }
        public double Price { get; set; }
    }
}
