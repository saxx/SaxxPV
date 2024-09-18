using System.Diagnostics;
using Microsoft.Extensions.Options;
using SaxxPv.Web.Models.Options;
using SaxxPv.Web.Services.Sems;

namespace SaxxPv.Web.Services;

public class SemsToTableBackgroundJob(
    ILogger<SemsToTableBackgroundJob> logger,
    SemsToTableService semsToTableService,
    IOptions<TablesOptions> tablesOptions,
    IOptions<SemsOptions> semsOptions) : IHostedService, IDisposable
{
    private Timer? _timer;

    public Task StartAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("SemsToTableBackgroundJob running.");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        var watch = new Stopwatch();
        watch.Start();
        logger.LogInformation("SemsToTableBackgroundJob working ...");
        await semsToTableService.Run(logger, tablesOptions.Value, semsOptions.Value);
        logger.LogInformation($"SemsToTableBackgroundJob completed ({watch.ElapsedMilliseconds:N0}ms).");
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("SemsToTableBackgroundJob is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
