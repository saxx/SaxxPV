using System.Globalization;
using SaxxPv.Web.Models.Options;
using SaxxPv.Web.Services;
using SaxxPv.Web.Services.Sems;
using SaxxPv.Web.Services.Tables;
using SaxxPv.Web.ViewModels.Home;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TablesOptions>(builder.Configuration.GetSection("Tables"));
builder.Services.Configure<SemsOptions>(builder.Configuration.GetSection("Sems"));
builder.Services.AddTransient<TablesClient>();
builder.Services.AddTransient<SemsToTableService>();
builder.Services.AddTransient<DayViewModelFactory>();
builder.Services.AddTransient<MonthViewModelFactory>();
builder.Services.AddTransient<PricingService>();
builder.Services.AddHostedService<SemsToTableBackgroundJob>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

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
app.Run();
