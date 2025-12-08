using Adliance.AspNetCore.Buddy.Storage;
using Adliance.AspNetCore.Buddy.Storage.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SaxxPv.Web.Models.Options;

namespace SaxxPv.Web.Tests;

public static class Mock
{
    public static IOptions<TablesOptions> TablesOptions => GetOptions<TablesOptions>("Tables");
    public static IOptions<SemsOptions> SemsOptions => GetOptions<SemsOptions>("Sems");
    
    public static IStorage Storage => new AzureStorage(GetOptions<DefaultStorageConfiguration>("Storage").Value);

    private static IOptions<T> GetOptions<T>(string sectionName) where T : class, new()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var options = new T();
        configuration.Bind(sectionName, options);
        return Options.Create(options);
    }
}