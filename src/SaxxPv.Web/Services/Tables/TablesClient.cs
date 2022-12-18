using Adliance.AspNetCore.Buddy.Extensions;
using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using SaxxPv.Web.Models.Options;
using SaxxPv.Web.Models.Tables;

namespace SaxxPv.Web.Services.Tables;

public class TablesClient
{
    private readonly TablesOptions _tablesOptions;

    public TablesClient(IOptions<TablesOptions> tablesOptions)
    {
        _tablesOptions = tablesOptions.Value;
    }
    
    public IList<SemsRow> LoadForDay(DateTime day, ILogger? log)
    {
        var start = day.Date;
        var end = start.AddDays(1).AddSeconds(-1);
        return Load(start, end, log);
    }

    public IList<SemsRow> LoadForMonth(DateTime day, ILogger? log)
    {
        var start = new DateTime(day.Year, day.Month, 1);
        var end = start.AddMonths(1).AddSeconds(-1);
        return Load(start, end, log);
    }

    public IList<SemsRow> Load(DateTime start, DateTime end, ILogger? log)
    {
        var tablesClient = new TableClient(_tablesOptions.TablesConnectionString, _tablesOptions.SemsTableName);
        log?.LogInformation($"Loading data for {start:yyyy-MM-dd} to {end:yyyy-MM-dd} from SEMS-Portal tables ...");

        var reverseTicksStart = DateTime.MaxValue.Ticks - start.AddSeconds(30).CetToUtc().Ticks; // add a minute extra because sometimes a few seconds spill over into the new day
        var reverseTicksEnd = DateTime.MaxValue.Ticks - end.AddSeconds(-30).CetToUtc().Ticks;

        var filter = $"RowKey le '{reverseTicksStart}' and RowKey gt '{reverseTicksEnd}'";
        var rows = tablesClient.Query<TableEntity>(filter).ToList();
        var semsEntries = rows.Select(x => new SemsRow(x)).ToList();
        return semsEntries;
    }
}