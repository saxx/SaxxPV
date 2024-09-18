using Microsoft.Extensions.Logging.Abstractions;
using SaxxPv.Web.Services.Sems;
using SaxxPv.Web.Services.Tables;
using Xunit;

namespace SaxxPv.Web.Tests.Services.Sems;

public class SemsToTableServiceTest
{
    [Fact]
    public async Task Can_Fetch_from_Sems_and_Store_in_Table()
    {
        var semsOptions = Mock.SemsOptions.Value;
        var tableOptions = Mock.TablesOptions;

        var tablesClient = new TablesClient(tableOptions);
        var numberOfRows = tablesClient.LoadSemsForDay(DateOnly.FromDateTime(DateTime.UtcNow), NullLogger.Instance);

        var service = new SemsToTableService();
        await service.Run(NullLogger.Instance, tableOptions.Value, semsOptions);

        var newNumberOfRows = tablesClient.LoadSemsForDay(DateOnly.FromDateTime(DateTime.UtcNow), NullLogger.Instance);
        Assert.Equal(numberOfRows.Count + 1, newNumberOfRows.Count);
    }
}