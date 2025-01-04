using System.Globalization;
using Azure.Data.Tables;

namespace SaxxPv.Web.Models.Tables;

public class PricingRow
{
    public PricingRow(TableEntity entity)
    {
        From = DateTime.Parse(entity.PartitionKey, CultureInfo.InvariantCulture);
        To = DateTime.Parse(entity.RowKey, CultureInfo.InvariantCulture);
        BuyPrice = GetValue(entity, "BuyPrice");
        SellPrice = GetValue(entity, "SellPrice");
    }

    public PricingRow(DateTime dateTime)
    {
        From = dateTime.Date;
        To = dateTime.Date;
    }

    private static double GetValue(TableEntity entity, string key)
    {
        if (entity.ContainsKey(key) && entity[key] is double d) return d;
        if (entity.ContainsKey(key) && entity[key] is int i) return i;
        return 0;
    }

    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
}
