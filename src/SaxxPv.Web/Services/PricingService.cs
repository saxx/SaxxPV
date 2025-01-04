using Microsoft.EntityFrameworkCore;
using SaxxPv.Web.Models.Database;

namespace SaxxPv.Web.Services;

public class PricingService(Db db)
{
    private readonly Dictionary<DateOnly, Pricing> _cache = new();

    public async Task<Pricing> LoadPricingEntry(DateOnly day)
    {
        if (_cache.TryGetValue(day, out var p)) return p;

        var d = day.ToDateTime(new TimeOnly(0, 0), DateTimeKind.Utc);
        var result = await db.Pricings
            .AsNoTracking()
            .OrderByDescending(x => x.From)
            .FirstOrDefaultAsync(x => d >= x.From && d <= x.To);
        if (result == null) result = new Pricing();
        _cache[day] = result;
        return result;
    }

    public async Task<double> CalculateBuyPrice(DateOnly day, double kwh)
    {
        return kwh * (await LoadPricingEntry(day)).BuyPrice / 100;
    }

    public async Task<double> CalculateSellPrice(DateOnly day, double kwh)
    {
        return kwh * (await LoadPricingEntry(day)).SellPrice / 100;
    }
}
