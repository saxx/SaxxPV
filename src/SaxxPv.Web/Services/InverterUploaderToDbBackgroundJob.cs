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
            var oldReading = await db.Readings
                .OrderByDescending(x => x.DateTime)
                .FirstOrDefaultAsync();

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

                DateTime = r.DateTime
            };

            if (oldReading != null && oldReading.DateTime > newReading.DateTime)
            {
                context.WriteLine("Reading is outdated.");
                continue;
            }

            if (oldReading == null || !newReading.Equals(oldReading))
            {
                context.WriteLine("Saving new reading to database ...");
                context.WriteLine(newReading.ToString());
                await db.AddAsync(newReading);
            }
            else
            {
                oldReading.DateTime = newReading.DateTime;
                context.WriteLine("Reading already exists in database.");
            }

            await db.SaveChangesAsync();
        }
    }
}
