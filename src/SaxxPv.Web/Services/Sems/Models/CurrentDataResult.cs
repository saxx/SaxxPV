using System.Text.Json.Serialization;

namespace SaxxPv.Web.Services.SemsClient.Models;

public class CurrentDataResult
{
    [JsonPropertyName("hasError")] public bool? HasError { get; set; }
    [JsonPropertyName("msg")] public string? Message { get; set; }
    [JsonPropertyName("data")] public DataContainer? Data { get; set; }

    public class DataContainer
    {
        [JsonPropertyName("info")] public Station? Station { get; set; }
        [JsonPropertyName("kpi")] public Kpi? Kpi { get; set; }
        [JsonPropertyName("powerflow")] public CurrentStatistics? Current { get; set; }
        [JsonPropertyName("energeStatisticsCharts")] public TodayStatistics? Today { get; set; }
    }

    public class Station
    {
        [JsonPropertyName("powerstation_id")] public Guid? Id { get; set; }
        [JsonPropertyName("time")] public string? Time { get; set; }
        [JsonPropertyName("date_format")] public string? DateFormat { get; set; }
        [JsonPropertyName("date_format_ym")] public string? DateFormatYm { get; set; }
        [JsonPropertyName("stationname")] public string? Name { get; set; }
        [JsonPropertyName("address")] public string? Address { get; set; }
        [JsonPropertyName("owner_name")] public string? OwnerName { get; set; }
        [JsonPropertyName("owner_phone")] public string? OwnerPhone { get; set; }
        [JsonPropertyName("owner_email")] public string? OwnerEmail { get; set; }
        [JsonPropertyName("battery_capacity")] public double? BatteryCapacity { get; set; }
        [JsonPropertyName("turnon_time")] public string? TurnOnTime { get; set; }
        [JsonPropertyName("create_time")] public string? CreateTime { get; set; }
        [JsonPropertyName("capacity")] public double? Capacity { get; set; }
        [JsonPropertyName("longitude")] public double? Longitude { get; set; }
        [JsonPropertyName("latitude")] public double? Latitude { get; set; }
        [JsonPropertyName("powerstation_type")] public string? Type { get; set; }
        [JsonPropertyName("status")] public int? Status { get; set; }
        [JsonPropertyName("is_stored")] public bool? IsStored { get; set; }
        [JsonPropertyName("is_powerflow")] public bool? IsPowerflow { get; set; }
        [JsonPropertyName("charts_type")] public int? ChartsType { get; set; }
        [JsonPropertyName("has_pv")] public bool? HasPv { get; set; }
        [JsonPropertyName("has_statistics_charts")] public bool? HasStatisticsCharts { get; set; }
        [JsonPropertyName("only_bps")] public bool? OnlyBps { get; set; }
        [JsonPropertyName("only_bpu")] public bool? OnlyBpu { get; set; }
        [JsonPropertyName("time_span")] public double? TimeSpan { get; set; }
        [JsonPropertyName("pr_value")] public string? PrValue { get; set; }
    }

    public class Kpi
    {
        [JsonPropertyName("month_generation")] public double? MonthGeneration { get; set; }
        [JsonPropertyName("pac")] public double? Pac { get; set; }
        [JsonPropertyName("power")] public double? Power { get; set; }
        [JsonPropertyName("total_power")] public double? TotalPower { get; set; }
        [JsonPropertyName("day_income")] public double? DayIncome { get; set; }
        [JsonPropertyName("total_income")] public double? TotalIncome { get; set; }
        [JsonPropertyName("yield_rate")] public double? YieldRate { get; set; }
        [JsonPropertyName("currency")] public string? Currency { get; set; }
    }

    public class CurrentStatistics
    {
        [JsonPropertyName("pv")] public string? Pv { get; set; }
        [JsonPropertyName("pvStatus")] public int? PvStatus { get; set; }
        [JsonPropertyName("bettery")] public string? Battery { get; set; } // übertrag in batterie in watt mit (W) am Ende
        [JsonPropertyName("betteryStatus")] public int? BatteryStatus { get; set; }
        [JsonPropertyName("betteryStatusStr")] public string? BatteryStatusStr { get; set; }
        [JsonPropertyName("load")] public string? Load { get; set; }
        [JsonPropertyName("loadStatus")] public int? LoadStatus { get; set; }
        [JsonPropertyName("grid")] public string? Grid { get; set; }
        [JsonPropertyName("soc")] public int? Soc { get; set; } // batterie in %
        [JsonPropertyName("socText")] public string? SocText { get; set; }
        [JsonPropertyName("hasEquipment")] public bool? HasEquipment { get; set; }
        [JsonPropertyName("gridStatus")] public int? GridStatus { get; set; } // =1 wenn grid läuft
        [JsonPropertyName("isBpuAndInverterNoBattery")] public bool? IsBpuAndInverterNoBattery { get; set; }
        [JsonPropertyName("isMoreBettery")] public bool? IsMoreBattery { get; set; }
    }

    public class TodayStatistics
    {
        [JsonPropertyName("sum")] public double? Sum { get; set; }
        [JsonPropertyName("buy")] public double? Buy { get; set; }
        [JsonPropertyName("buyPercent")] public double? BuyPercent { get; set; }
        [JsonPropertyName("sell")] public double? Sell { get; set; }
        [JsonPropertyName("sellPercent")] public double? SellPercent { get; set; }
        [JsonPropertyName("selfUseOfPv")] public double? SelfUseOfPv { get; set; }
        [JsonPropertyName("consumptionOfLoad")] public double? ConsumptionOfLoad { get; set; }
        [JsonPropertyName("chartsType")] public int? ChartsType { get; set; }
        [JsonPropertyName("hasPv")] public bool? HasPv { get; set; }
        [JsonPropertyName("hasCharge")] public bool? HasCharge { get; set; }
        [JsonPropertyName("charge")] public double? Charge { get; set; }
        [JsonPropertyName("disCharge")] public double? Discharge { get; set; }
    }
}
