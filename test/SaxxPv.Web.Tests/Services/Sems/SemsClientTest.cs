using SaxxPv.Web.Services.Sems;
using Xunit;

namespace SaxxPv.Web.Tests.Services.Sems;

public class SemsClientTest
{
    [Fact(Skip = "Skip in pipeline.")]
    public async Task Can_Authenticate()
    {
        var semsOptions = Mock.SemsOptions.Value;
        var semsClient = new SemsClient();
        var authenticationResult = await semsClient.Authenticate(semsOptions.Username, semsOptions.Password);
        Assert.False(authenticationResult.HasError);
        Assert.NotEmpty(authenticationResult.Data!.Token!);
    }

    [Fact(Skip = "Skip in pipeline.")]
    public async Task Can_Fetch_Plants()
    {
        var semsOptions = Mock.SemsOptions.Value;
        var semsClient = new SemsClient();
        await semsClient.Authenticate(semsOptions.Username, semsOptions.Password);
        var plantsResult = await semsClient.FetchStations();
        Assert.False(plantsResult.HasError);
        Assert.NotEmpty(plantsResult.Stations);
    }

    [Fact(Skip = "Skip in pipeline.")]
    public async Task Can_Fetch_Current_Data()
    {
        var semsOptions = Mock.SemsOptions.Value;
        var semsClient = new SemsClient();
        await semsClient.Authenticate(semsOptions.Username, semsOptions.Password);
        var plants = await semsClient.FetchStations();
        var currentDataResult = await semsClient.FetchCurrentData(plants.Stations.First().Id!);
        Assert.False(currentDataResult.HasError);
        Assert.InRange(currentDataResult.Data!.Today!.Sum!.Value, 0.0, 100);
    }
}