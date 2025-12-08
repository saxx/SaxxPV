namespace SaxxPv.Web.Services.InverterUploader.Models;

public class Result
{
    public required DateTime DateTime { get; set; }

    public required double CurrentLoad { get; set; }
    public required double CurrentPv { get; set; }
    public required double CurrentGrid { get; set; }
    public required double CurrentBattery { get; set; }
    public required double CurrentBatterySoc { get; set; }

    public required double DayTotal { get; set; }
    public required double DayBought { get; set; }
    public required double DaySold { get; set; }
    public required double DayConsumption { get; set; }
    public required double DaySelfUse { get; set; }
}
