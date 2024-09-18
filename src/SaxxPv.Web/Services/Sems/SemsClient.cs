using System.Text.Json;
using SaxxPv.Web.Services.SemsClient.Models;

namespace SaxxPv.Web.Services.Sems;

public class SemsClient : IDisposable
{
    private HttpClient _client;

    public SemsClient()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://www.semsportal.com");
        SetAuthenticationHeader(null, null, null);
    }

    public async Task<AuthenticationResult> Authenticate(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new Exception("No username provided.");
        if (string.IsNullOrWhiteSpace(password)) throw new Exception("No password provided.");

        var response = await _client.PostAsJsonAsync("api/v1/Common/CrossLogin", new
        {
            account = username,
            pwd = password
        });
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<AuthenticationResult>(responseString) ?? throw new Exception("Unable to deserialize.");

        if (string.IsNullOrEmpty(responseObject.Api)) throw new Exception("No API endpoint provided");
        if (string.IsNullOrEmpty(responseObject.Data?.Token)) throw new Exception("No token provided");
        if (responseObject.Data?.Timestamp == null) throw new Exception("No timestamp provided");
        if (responseObject.Data?.Uid == null) throw new Exception("No UID provided");

        _client.Dispose();
        _client = new HttpClient();
        _client.BaseAddress = new Uri(responseObject.Api);
        SetAuthenticationHeader(responseObject.Data.Timestamp, responseObject.Data.Uid, responseObject.Data.Token);
        return responseObject;
    }

    public async Task<StationsResult> FetchStations()
    {
        var response = await _client.PostAsJsonAsync("PowerStationMonitor/QueryPowerStationMonitorForApp", new
        {
            page_size = 50,
            orderby = "",
            powerstation_status = "",
            key = "",
            page_index = "1",
            powerstation_id = "",
            powerstation_type = ""
        });
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<StationsResult>(responseString) ?? throw new Exception("Unable to deserialize.");
        return responseObject;
    }

    public async Task<CurrentDataResult> FetchCurrentData(string stationId)
    {
        var response = await _client.PostAsJsonAsync("v2/PowerStation/GetMonitorDetailByPowerstationId", new
        {
            powerStationId = stationId
        });
        var responseString = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        var responseObject = JsonSerializer.Deserialize<CurrentDataResult>(responseString) ?? throw new Exception("Unable to deserialize.");
        return responseObject;
    }

    private void SetAuthenticationHeader(long? timestamp, Guid? uid, string? token)
    {
        if (timestamp == null || uid == null || token == null)
        {
            _client.DefaultRequestHeaders.Add("Token", JsonSerializer.Serialize(new
            {
                client = "ios",
                version = "v2.1.0",
                language = "en",
            }));
        }
        else
        {
            _client.DefaultRequestHeaders.Add("Token", JsonSerializer.Serialize(new
            {
                client = "ios",
                version = "v2.1.0",
                language = "en",
                timestamp,
                uid,
                token
            }));
        }
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
