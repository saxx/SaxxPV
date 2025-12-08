using SaxxPv.Web.Services.InverterUploader;
using Xunit;

namespace SaxxPv.Web.Tests.Services.InverterUploader;

public class InverterUploaderServiceTest
{
    [Fact(Skip = "Skip in pipeline.")]
    public async Task Can_Fetch_Readings()
    {
        var storage = Mock.Storage;
        var service = new InverterUploaderService(storage);
        var readings = await service.GetReadings();
        Assert.NotEmpty(readings);
    }

    [Fact(Skip = "Skip in pipeline.")]
    public async Task Can_Fetch_Results()
    {
        var storage = Mock.Storage;
        var service = new InverterUploaderService(storage);
        var results = await service.GetResults();
        Assert.NotEmpty(results);
        Assert.All(results, x => Assert.True(x.DateTime >= DateTime.UtcNow.AddDays(-3)));
        Assert.All(results, x => Assert.True(x.DateTime < DateTime.UtcNow));
        Assert.All(results, x => Assert.InRange(x.CurrentBatterySoc, 1, 100));
        Assert.All(results, x => Assert.InRange(x.CurrentBattery, -5000, 5000));
        Assert.All(results, x => Assert.InRange(x.DaySelfUse, 0, 40));
        Assert.All(results, x => Assert.InRange(x.DayBought, 0, 40));
        Assert.All(results, x => Assert.InRange(x.DayConsumption, 4, 50));
        Assert.All(results, x => Assert.InRange(x.DaySold, 0, 50));
        Assert.All(results, x => Assert.InRange(x.DayTotal, 0, 50));
        Assert.All(results, x => Assert.InRange(x.CurrentGrid, 250, 8000));
        Assert.All(results, x => Assert.InRange(x.CurrentLoad, 250, 8000));
        Assert.All(results, x => Assert.InRange(x.CurrentPv, 0, 8000));
    }
}