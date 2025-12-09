using System.Text.Json.Serialization;

namespace SaxxPv.Web.Services.SemsClient.Models;

public class StationsResult
{
    [JsonPropertyName("hasError")] public bool? HasError { get; set; }
    [JsonPropertyName("msg")] public string? Message { get; set; }
    [JsonPropertyName("code")] public int? Code { get; set; }

    [JsonPropertyName("data")] public IList<Station> Stations { get; set; } = new List<Station>();

    public class Station
    {
        [JsonPropertyName("powerstation_id")] public string? Id { get; set; }
        [JsonPropertyName("powercontrol_status")] public int? PowercontrolStatus { get; set; }
        [JsonPropertyName("stationname")] public string? Name { get; set; }
        [JsonPropertyName("first_letter")] public string? FirstLetter { get; set; }
        [JsonPropertyName("adcode")] public string? AddressCode { get; set; }
        [JsonPropertyName("location")] public string? Location { get; set; }
        [JsonPropertyName("status")] public int? Status { get; set; }
        [JsonPropertyName("pac")] public double? Pac { get; set; }
        [JsonPropertyName("capacity")] public double? Capacity { get; set; }
        [JsonPropertyName("eday")] public double? EgressDay { get; set; }
        [JsonPropertyName("emonth")] public double? EgressMonth { get; set; }
        [JsonPropertyName("eday_income")] public double? EgressDayIncome { get; set; }
        [JsonPropertyName("etotal")] public double? EgressTotal { get; set; }
        [JsonPropertyName("powerstation_type")] public string? Type { get; set; }
        [JsonPropertyName("pre_org_id")] public string? PreOrgId { get; set; }
        [JsonPropertyName("org_id")] public string? OrgId { get; set; }
        [JsonPropertyName("longitude")] public string? Longitude { get; set; }
        [JsonPropertyName("latitude")] public string? Latitude { get; set; }
        [JsonPropertyName("pac_kw")] public double? PacKw { get; set; }
        [JsonPropertyName("to_hour")] public double? ToHour { get; set; }
        [JsonPropertyName("currency")] public string? Currency { get; set; }
        [JsonPropertyName("yield_rate")] public double? YieldRate { get; set; }
        [JsonPropertyName("is_stored")] public bool? IsStored { get; set; }

        [JsonPropertyName("weather")] public WeatherContainer? Weather { get; set; }
    }
}

public class WeatherContainer
{
    [JsonPropertyName("HeWeather6")] public IList<Weather> Weathers { get; set; } = new List<Weather>();
}

public class Weather
{
    [JsonPropertyName("basic")] public WeatherBasic? Basic { get; set; }
    [JsonPropertyName("update")] public WeatherUpdate? Update { get; set; }
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("now")] public WeatherNow? Now { get; set; }
}

public class WeatherBasic
{
    [JsonPropertyName("cid")] public string? Cid { get; set; }
    [JsonPropertyName("location")] public string? Location { get; set; }
    [JsonPropertyName("parent_city")] public string? ParentCity { get; set; }
    [JsonPropertyName("admin_area")] public string? AdminArea { get; set; }
    [JsonPropertyName("cnty")] public string? Country { get; set; }
    [JsonPropertyName("lat")] public string? Latitude { get; set; }
    [JsonPropertyName("lon")] public string? Longitude { get; set; }
    [JsonPropertyName("tz")] public string? Timezone { get; set; }
}

public class WeatherUpdate
{
    [JsonPropertyName("loc")] public string? Loc { get; set; }
    [JsonPropertyName("utc")] public string? Utc { get; set; }
}

public class WeatherNow
{
    [JsonPropertyName("cloud")] public string? Cloud { get; set; }
    [JsonPropertyName("cond_code")] public string? ConditionCode { get; set; }
    [JsonPropertyName("cond_txt")] public string? CondidtionText { get; set; }
    [JsonPropertyName("fl")] public string? Fl { get; set; }
    [JsonPropertyName("hum")] public string? Hum { get; set; }
    [JsonPropertyName("pcpn")] public string? Pcpn { get; set; }
    [JsonPropertyName("pres")] public string? Pres { get; set; }
    [JsonPropertyName("tmp")] public string? Tmp { get; set; }
    [JsonPropertyName("vis")] public string? Vis { get; set; }
    [JsonPropertyName("wind_deg")] public string? WindDeg { get; set; }
    [JsonPropertyName("wind_dir")] public string? WindDirection { get; set; }
    [JsonPropertyName("wind_sc")] public string? WindSc { get; set; }
    [JsonPropertyName("wind_spd")] public string? WindSpeed { get; set; }
}
