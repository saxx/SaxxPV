#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()

namespace SaxxPv.Web.Models.Database;

public class Reading
{
    public DateTime DateTime { get; set; }

    public double CurrentLoad { get; set; }
    public double CurrentPv { get; set; }
    public double CurrentGrid { get; set; }
    public double CurrentBattery { get; set; }
    public double CurrentBatterySoc { get; set; }

    public double DayTotal { get; set; }
    public double DayBought { get; set; }
    public double DaySold { get; set; }
    public double DayConsumption { get; set; }
    public double DaySelfUse { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Reading r) return false;
        if (Math.Abs(r.CurrentLoad - CurrentLoad) > 0.01) return false;
        if (Math.Abs(r.CurrentPv - CurrentPv) > 0.01) return false;
        if (Math.Abs(r.CurrentGrid - CurrentGrid) > 0.01) return false;
        if (Math.Abs(r.CurrentBatterySoc - CurrentBatterySoc) > 0.01) return false;
        if (Math.Abs(r.DayTotal - DayTotal) > 0.01) return false;
        if (Math.Abs(r.DayBought - DayBought) > 0.01) return false;
        if (Math.Abs(r.DaySold - DaySold) > 0.01) return false;
        if (Math.Abs(r.DayConsumption - DayConsumption) > 0.01) return false;
        if (Math.Abs(r.DaySelfUse - DaySelfUse) > 0.01) return false;
        return true;
    }

    public override string ToString()
    {
        return $"Date: {DateTime:yyyy-MM-dd HH:mm:ss}\n" +
               $"Current Load: {CurrentLoad:0.00} W\n" +
               $"Current PV: {CurrentPv:0.00} W\n" +
               $"Current Grid: {CurrentGrid:0.00} W\n" +
               $"Current Battery: {CurrentBattery:0.00} W\n" +
               $"Current Battery SOC: {CurrentBatterySoc:0.00} %\n" +
               $"Day Total: {DayTotal:0.00} kWh\n" +
               $"Day Bought: {DayBought:0.00} kWh\n" +
               $"Day Sold: {DaySold:0.00} kWh\n" +
               $"Day Consumption: {DayConsumption:0.00} kWh\n" +
               $"Day Self-Use: {DaySelfUse:0.00} kWh";
    }
}
