using System.Globalization;
using Azure.Data.Tables;
using SaxxPv.Web.Models.Options;
using SaxxPv.Web.Models.Tables;

namespace SaxxPv.Web.Services.Sems;

public class SemsToTableService
{
    public async Task Run(ILogger log, TablesOptions tablesOptions, SemsOptions semsOptions)
    {
        log.LogInformation("Loading SEMS data ...");
        var semsClient = new SemsClient();
        await semsClient.Authenticate(semsOptions.Username, semsOptions.Password);
        var stations = await semsClient.FetchStations();
        var data = await semsClient.FetchCurrentData(stations.Stations.First().Id!);
        if (data.Data?.Station?.Id == null) throw new Exception("No station ID available.");
        if (data.Data?.Today == null) throw new Exception("No data for today available.");
        if (data.Data?.Current == null) throw new Exception("No current data available.");

        log.LogInformation("Saving data to table storage ...");
        var tablesClient = new TableClient(tablesOptions.ConnectionString, tablesOptions.SemsTableName);
        var semsEntry = new SemsRow(data.Data.Station.Id.ToString() ?? "", DateTime.UtcNow)
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
        };

        await tablesClient.AddEntityAsync(semsEntry.ToTableEntity());
        return;

        double? ParseWatts(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            s = s.Replace("(W)", "");
            if (double.TryParse(s, CultureInfo.InvariantCulture, out var d)) return d;
            return null;
        }
    }
}
