using Hangfire.Console;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;
using SaxxPv.Web.Models.Database;
using SaxxPv.Web.Services.InverterUploader;

namespace SaxxPv.Web.Services;

public class InverterUploaderToDbBackgroundJob(IServiceProvider services)
{
    public async Task Run(PerformContext? context)
    {
        using var scope = services.CreateScope();
        await using var db = scope.ServiceProvider.GetRequiredService<Db>();
        var inverterUploader = scope.ServiceProvider.GetRequiredService<InverterUploaderService>();

        context.WriteLine("Fetching results ...");
        var results = await inverterUploader.GetResults(true);

        foreach (var r in results.OrderBy(x => x.DateTime))
        {
            var lastReading = await db.Readings.AsNoTracking()
                .OrderByDescending(x => x.DateTime)
                .FirstOrDefaultAsync();

            var day = r.DateTime.Date;
            var firstReadingOfDay = await db.Readings
                .Where(x => x.DateTime >= day && x.TotalImport != null && x.TotalExport != null)
                .OrderBy(x => x.DateTime)
                .Select(x=>new
                {
                    x.TotalImport,
                    x.TotalExport
                }).FirstOrDefaultAsync();

            var newReading = new Reading
            {
                CurrentLoad = r.CurrentLoad,
                CurrentPv = r.CurrentPv,
                CurrentGrid = r.CurrentGrid,
                CurrentBattery = r.CurrentBattery,
                CurrentBatterySoc = r.CurrentBatterySoc,

                DayTotal = r.DayTotal,
                DayBought = r.DayBought,
                DaySold = r.DaySold,
                DayConsumption = r.DayConsumption,
                DaySelfUse = r.DaySelfUse,
                DayBatteryCharge = r.DayBatteryCharge,
                DayBatteryDischarge = r.DayBatteryDischarge,

                TotalImport = r.TotalImport,
                TotalExport = r.TotalExport,

                DateTime = r.DateTime
            };

            if (firstReadingOfDay != null)
            {
                if (firstReadingOfDay.TotalImport.HasValue && newReading.TotalImport.HasValue)
                {
                    newReading.DayBought = Math.Round(newReading.TotalImport.Value - firstReadingOfDay.TotalImport.Value, 1, MidpointRounding.AwayFromZero);
                }

                if (firstReadingOfDay.TotalExport.HasValue && newReading.TotalExport.HasValue)
                {
                    newReading.DaySold = Math.Round(newReading.TotalExport.Value - firstReadingOfDay.TotalExport.Value, 1, MidpointRounding.AwayFromZero);
                    newReading.DaySelfUse = Math.Round(newReading.DayTotal - newReading.DaySold, 1, MidpointRounding.AwayFromZero);
                }
            }

            if (lastReading != null && lastReading.DateTime > newReading.DateTime)
            {
                context.WriteLine("Reading is outdated.");
                continue;
            }

            if (lastReading == null || !newReading.Equals(lastReading))
            {
                context.WriteLine("Saving new reading to database ...");
                context.WriteLine(newReading.ToString());
                await db.AddAsync(newReading);
            }
            else
            {
                lastReading.DateTime = newReading.DateTime;
                context.WriteLine("Reading already exists in database.");
            }

            await db.SaveChangesAsync();
        }

        context.WriteLine("Done. Goodbye.");
    }
}
