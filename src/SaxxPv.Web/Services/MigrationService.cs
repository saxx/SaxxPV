using System.Globalization;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using SaxxPv.Web.Models.Database;
using SaxxPv.Web.Services.Tables;

namespace SaxxPv.Web.Services;

public class MigrationService(IServiceProvider services)
{
    public async Task MigratePricingData(PerformContext? context)
    {
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

        using var scope = services.CreateScope();
        await using var db = scope.ServiceProvider.GetRequiredService<Db>();
        var tablesClient = scope.ServiceProvider.GetRequiredService<TablesClient>();

        var deletedRows = await db.Pricings.ExecuteDeleteAsync();
        context.WriteLine($"Deleted {deletedRows} rows from database.");

        context.WriteLine("Loading tables data ...");
        var rows = tablesClient.LoadPricing(NullLogger.Instance);
        context.WriteLine($"\t{rows.Count} rows loaded.");

        context.WriteLine("Inserting new data into database ...");
        foreach (var r in rows)
        {
            await db.Pricings.AddAsync(new Pricing
            {
                From = r.From,
                To = r.To,
                BuyPrice = r.BuyPrice,
                SellPrice = r.SellPrice
            });
        }

        await db.SaveChangesAsync();
    }

    public async Task MigrateReadingData(PerformContext? context)
    {
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

        using var scope = services.CreateScope();
        await using var db = scope.ServiceProvider.GetRequiredService<Db>();
        var tablesClient = scope.ServiceProvider.GetRequiredService<TablesClient>();

        var d = new DateOnly(2022, 1, 1);
        while (d < new DateOnly(2025, 2, 1))
        {
            context.WriteLine($"Working on {d.AddMonths(1):yyyy-MM} ...");

            context.WriteLine($"\tLoading tables data ...");
            var rows = tablesClient.LoadSemsForMonth(d, NullLogger.Instance);
            context.WriteLine($"\t\t{rows.Count} rows loaded.");
            rows = rows.DistinctBy(x => x.DateTime).ToList();
            context.WriteLine($"\t\t{rows.Count} distinct rows.");

            var min = new DateTime(d, new TimeOnly(0, 0, 0), DateTimeKind.Utc);
            d = d.AddMonths(1);
            if (!rows.Any()) continue;
            var max = new DateTime(d, new TimeOnly(0, 0, 0), DateTimeKind.Utc);
            var rowsMax = rows.Max(x => x.DateTime);
            if (max > rowsMax) max = rowsMax;
            max = max.AddSeconds(1);
            var deletedRows = await db.Readings.Where(x => x.DateTime >= min && x.DateTime < max).ExecuteDeleteAsync();
            context.WriteLine($"\tDeleted {deletedRows} rows between {min} and {max} from database.");

            context.WriteLine("\tInserting new data into database ...");
            foreach (var r in rows.OrderBy(x => x.DateTime))
            {
                var newReading = new Reading
                {
                    CurrentGrid = r.CurrentGrid,
                    CurrentLoad = r.CurrentLoad,
                    CurrentPv = r.CurrentPv,
                    CurrentBattery = r.CurrentBattery,
                    CurrentBatterySoc = r.CurrentBatterySoc,
                    DayBought = r.DayBought,
                    DayConsumption = r.DayConsumption,
                    DaySelfUse = r.DaySelfUse,
                    DateTime = r.DateTime,
                    DaySold = r.DaySold,
                    DayTotal = r.DayTotal
                };

                db.Readings.Add(newReading);
            }

            var inserted = await db.SaveChangesAsync();
            context.WriteLine($"\t\t{inserted} rows inserted.");
        }
    }
}
