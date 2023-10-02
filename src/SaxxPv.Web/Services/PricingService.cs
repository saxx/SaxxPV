using SaxxPv.Web.Models.Tables;
using SaxxPv.Web.Services.Tables;

namespace SaxxPv.Web.Services;

public class PricingService
{
    private readonly ILogger<PricingService> _logger;
    private readonly TablesClient _tablesClient;

    public PricingService(ILogger<PricingService> logger, TablesClient tablesClient)
    {
        _logger = logger;
        _tablesClient = tablesClient;
    }

    private IList<PricingRow>? _pricingRows;

    public PricingRow LoadPricingEntry(DateOnly day)
    {
        if (_pricingRows == null) _pricingRows = _tablesClient.LoadPricing(_logger);

        var from = day.ToDateTime(new TimeOnly());
        var to = from;

        var entry = _pricingRows.FirstOrDefault(x => x.From.Date <= from.Date && x.To.Date.AddDays(1) > to.Date);
        if (entry == null) return new PricingRow(day.ToDateTime(new TimeOnly()));
        return entry;
    }

    public double CalculateBuyPrice(DateOnly day, double kwh)
    {
        return kwh  * LoadPricingEntry(day).BuyPrice / 100;
    }

    public double CalculateSellPrice(DateOnly day, double kwh)
    {
        return kwh  * LoadPricingEntry(day).SellPrice / 100;
    }
}
