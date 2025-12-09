using System.Text.Json.Serialization;

namespace SaxxPv.Web.Services.SemsClient.Models;

public class AuthenticationResult
{
    [JsonPropertyName("hasError")] public bool? HasError { get; set; }
    [JsonPropertyName("msg")] public string? Message { get; set; }
    [JsonPropertyName("code")] public int? Code { get; set; }
    [JsonPropertyName("api")] public string? Api { get; set; }
    [JsonPropertyName("data")] public AuthenticationData? Data { get; set; }

    public class AuthenticationData
    {
        [JsonPropertyName("uid")] public Guid? Uid { get; set; }
        [JsonPropertyName("timestamp")] public long? Timestamp { get; set; }
        [JsonPropertyName("token")] public string? Token { get; set; }
        [JsonPropertyName("client")] public string? Client { get; set; }
        [JsonPropertyName("Version")] public string? Version { get; set; }
        [JsonPropertyName("Language")] public string? Language { get; set; }
    }
}
