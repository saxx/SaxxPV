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
    public double DayConsumption { get; set; }

    public double DayBought { get; set; }
    public double DaySelfUse { get; set; }
    public double DaySold { get; set; }

    public double? DayBatteryCharge { get; set; }
    public double? DayBatteryDischarge { get; set; }
    public double? TotalImport { get; set; }
    public double? TotalExport { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Reading r) return false;
        if (Math.Abs(r.CurrentLoad - CurrentLoad) > 10) return false; // W
        if (Math.Abs(r.CurrentPv - CurrentPv) > 10) return false; // W
        if (Math.Abs(r.CurrentGrid - CurrentGrid) > 10) return false; // W
        if (Math.Abs(r.CurrentBatterySoc - CurrentBatterySoc) > 1) return false; // %

        if (Math.Abs(r.DayTotal - DayTotal) > 0.1) return false; // kwH
        if (Math.Abs(r.DayBought - DayBought) > 0.1) return false; // kwH
        if (Math.Abs(r.DaySold - DaySold) > 0.1) return false; // kwH
        if (Math.Abs(r.DayConsumption - DayConsumption) > 0.1) return false; // kwH
        if (Math.Abs(r.DaySelfUse - DaySelfUse) > 0.1) return false; // kwH


        if (r.DayBatteryCharge == null && DayBatteryCharge != null) return false;
        if (r.DayBatteryCharge != null && DayBatteryCharge == null) return false;
        if (r.DayBatteryCharge != null && DayBatteryCharge != null && Math.Abs(r.DayBatteryCharge.Value - DayBatteryCharge.Value) > 0.1) return false; // kwH

        if (r.DayBatteryDischarge == null && DayBatteryDischarge != null) return false;
        if (r.DayBatteryDischarge != null && DayBatteryDischarge == null) return false;
        if (r.DayBatteryDischarge != null && DayBatteryDischarge != null && Math.Abs(r.DayBatteryDischarge.Value - DayBatteryDischarge.Value) > 0.1) return false; // kwH

        if (r.TotalExport == null && TotalExport != null) return false;
        if (r.TotalExport != null && TotalExport == null) return false;
        if (r.TotalExport != null && TotalExport != null && Math.Abs(r.TotalExport.Value - TotalExport.Value) > 0.1) return false; // kwH

        if (r.TotalImport == null && TotalImport != null) return false;
        if (r.TotalImport != null && TotalImport == null) return false;
        if (r.TotalImport != null && TotalImport != null && Math.Abs(r.TotalImport.Value - TotalImport.Value) > 0.1) return false; // kwH
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
               $"Day Self-Use: {DaySelfUse:0.00} kWh" +
               $"Day Battery Charge: {DayBatteryCharge:0.00} kWh" +
               $"Day Battery Discharge: {DayBatteryDischarge:0.00} kWh" +
               $"Total Import: {TotalImport:0.00} kWh" +
               $"Total Export: {TotalExport:0.00} kWh";
    }
}
