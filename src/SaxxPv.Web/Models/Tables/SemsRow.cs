using Adliance.Buddy.DateTime;
using Azure.Data.Tables;

namespace SaxxPv.Web.Models.Tables;

public class SemsRow
{
    public SemsRow(string stationId, DateTime dateTime)
    {
        StationId = stationId;
        DateTime = dateTime;
    }

    public SemsRow(TableEntity entity)
    {
        StationId = entity.PartitionKey;
        DateTime = (entity.Timestamp?.DateTime ?? DateTime.UtcNow).UtcToCet();

        CurrentLoad = GetValue(entity, "CurrentLoad");
        CurrentPv = GetValue(entity, "CurrentPv");
        CurrentGrid = GetValue(entity, "CurrentGrid");

        DayTotal = GetValue(entity, "DayTotal");
        DayBought = GetValue(entity, "DayBought");
        DaySold = GetValue(entity, "DaySold");
        DayConsumption = GetValue(entity, "DayConsumption");
        DaySelfUse = GetValue(entity, "DaySelfUse");

        if (CurrentLoad > CurrentPv) CurrentGrid *= -1;
    }

    private static double GetValue(TableEntity entity, string key)
    {
        if (entity.ContainsKey(key) && entity[key] is double d) return d;
        if (entity.ContainsKey(key) && entity[key] is int i) return i;
        return 0;
    }

    public TableEntity ToTableEntity()
    {
        return new TableEntity
        {
            PartitionKey = StationId,
            RowKey = $"{DateTime.MaxValue.Ticks - DateTime.Ticks}", // use "inverted ticks" which makes it especially easy to fetch the latest entry in a partition

            ["CurrentLoad"] = CurrentLoad,
            ["CurrentPv"] = CurrentPv,
            ["CurrentGrid"] = CurrentGrid,

            ["DayTotal"] = DayTotal,
            ["DayBought"] = DayBought,
            ["DaySold"] = DaySold,
            ["DayConsumption"] = DayConsumption,
            ["DaySelfUse"] = DaySelfUse
        };
    }

    public string StationId { get; set; }
    public DateTime DateTime { get; set; }


    public double CurrentLoad { get; set; }
    public double CurrentPv { get; set; }
    public double CurrentGrid { get; set; }

    public double DayTotal { get; set; }
    public double DayBought { get; set; }
    public double DaySold { get; set; }
    public double DayConsumption { get; set; }
    public double DaySelfUse { get; set; }
}
