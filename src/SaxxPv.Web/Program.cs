using System.Globalization;
using Hangfire;
using Hangfire.Console;
using Microsoft.EntityFrameworkCore;
using SaxxPv.Web;
using SaxxPv.Web.Models.Database;
using SaxxPv.Web.Models.Options;
using SaxxPv.Web.Services;
using SaxxPv.Web.ViewModels.Home;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TablesOptions>(builder.Configuration.GetSection("Tables"));
builder.Services.Configure<SemsOptions>(builder.Configuration.GetSection("Sems"));
builder.Services.AddTransient<DayViewModelFactory>();
builder.Services.AddTransient<MonthViewModelFactory>();
builder.Services.AddTransient<PricingService>();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Db>(options =>
{
    var connectionString = builder.Configuration.GetValue<string>("DatabaseConnectionString");
    options.UseSqlServer(connectionString);
});
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetValue<string>("DatabaseConnectionString"));
    config.UseConsole();
});
builder.Services.AddHangfireServer(x => { });
builder.Services.AddHealthChecks().AddDbContextCheck<Db>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
await using (var db = scope.ServiceProvider.GetRequiredService<Db>())
{
    await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();
app.UseHsts();
app.UseStatusCodePages();
app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    SupportedCultures = new List<CultureInfo>
    {
        new("en-GB")
    }
});
app.MapDefaultControllerRoute();
app.MapHealthChecks("/health");
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization =
    [
        new HangfireAuthorizationFilter()
    ]
});
RecurringJob.AddOrUpdate("Fetch_Reading", () => new SemsToDbBackgroundJob(app.Services).Run(null!), Cron.Minutely);
app.Run();
