using SaxxPv.Web.Services.Tables;
using Xunit;

namespace SaxxPv.Web.Tests.Services.Tables;

public class TablesClientTest
{
    [Fact]
    public void Can_Fetch_Sems_Data_For_Date_Range()
    {
        var service = new TablesClient(Mock.TablesOptions);
        var rows = service.LoadSems(DateTime.Parse("2022-10-01"), DateTime.Parse("2022-11-30"), null);
        Assert.Equal(25262, rows.Count);
    }

    [Fact]
    public void Can_Fetch_Sems_Data_For_Day()
    {
        var service = new TablesClient(Mock.TablesOptions);
        var rows = service.LoadSemsForDay(DateOnly.Parse("2022-10-01"), null);
        Assert.Equal(399, rows.Count);
    }

    [Fact]
    public void Can_Fetch_Sems_Data_For_Month()
    {
        var service = new TablesClient(Mock.TablesOptions);
        var rows = service.LoadSemsForMonth(DateOnly.Parse("2022-10-26"), null);
        Assert.Equal(12509, rows.Count);
    }
}