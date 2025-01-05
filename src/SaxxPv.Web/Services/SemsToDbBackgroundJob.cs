using System.Globalization;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SaxxPv.Web.Models.Database;
using SaxxPv.Web.Models.Options;

namespace SaxxPv.Web.Services;

public class SemsToDbBackgroundJob(IServiceProvider services)
{
    private static string? _stationIdCache;

    public async Task Run(PerformContext? context)
    {
        using var scope = services.CreateScope();
        await using var db = scope.ServiceProvider.GetRequiredService<Db>();
        var semsOptions = scope.ServiceProvider.GetRequiredService<IOptions<SemsOptions>>();

        context.WriteLine("Authenticate with SEMS ...");
        var semsClient = new Sems.SemsClient();
        await semsClient.Authenticate(semsOptions.Value.Username, semsOptions.Value.Password);

        if (_stationIdCache == null)
        {
            context.WriteLine("Loading stations from SEMS ...");
            var stations = await semsClient.FetchStations();
            _stationIdCache = stations.Stations.First().Id;
        }

        try
        {
            context.WriteLine("Loading current reading from SEMS ...");
            var data = await semsClient.FetchCurrentData(_stationIdCache!);
            if (data.Data?.Station?.Id == null) throw new Exception("No station ID available.");
            if (data.Data?.Today == null) throw new Exception("No data for today available.");
            if (data.Data?.Current == null) throw new Exception("No current data available.");

            context.WriteLine("Loading previous reading ...");
            var oldReading = await db.Readings
                .OrderByDescending(x => x.DateTime)
                .FirstOrDefaultAsync();

            var newReading = new Reading
            {
                CurrentLoad = ParseWatts(data.Data.Current.Load) ?? 0,
                CurrentPv = ParseWatts(data.Data.Current.Pv) ?? 0,
                CurrentGrid = ParseWatts(data.Data.Current.Grid) ?? 0,
                CurrentBattery = ParseWatts(data.Data.Current.Battery) ?? 0,
                CurrentBatterySoc = data.Data.Current.Soc ?? 0,

                DayTotal = data.Data.Today.Sum ?? 0,
                DayBought = data.Data.Today.Buy ?? 0,
                DaySold = data.Data.Today.Sell ?? 0,
                DayConsumption = data.Data.Today.ConsumptionOfLoad ?? 0,
                DaySelfUse = data.Data.Today.SelfUseOfPv ?? 0,

                DateTime = DateTime.UtcNow
            };

            if (data.Data.Current.BatteryStatus < 0) newReading.CurrentBattery *= -1;

            if (oldReading == null || !newReading.Equals(oldReading))
            {
                context.WriteLine("Saving new reading to database ...");
                context.WriteLine(newReading.ToString());
                await db.AddAsync(newReading);
                await db.SaveChangesAsync();
            }
            else
            {
                context.WriteLine("Reading already exists in database.");
            }
        }
        catch
        {
            _stationIdCache = null;
            throw;
        }
    }

    private static double? ParseWatts(string? s)
    {
        if (string.IsNullOrWhiteSpace(s)) return null;
        s = s.Replace("(W)", "");
        if (double.TryParse(s, CultureInfo.InvariantCulture, out var d)) return d;
        return null;
    }
}
