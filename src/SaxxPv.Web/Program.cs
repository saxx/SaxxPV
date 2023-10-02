using System.Globalization;
using SaxxPv.Web.Models.Options;
using SaxxPv.Web.Services;
using SaxxPv.Web.Services.Tables;
using SaxxPv.Web.ViewModels.Home;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TablesOptions>(builder.Configuration.GetSection("Tables"));
builder.Services.AddTransient<TablesClient>();
builder.Services.AddTransient<DayViewModelFactory>();
builder.Services.AddTransient<MonthViewModelFactory>();
builder.Services.AddTransient<PricingService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStatusCodePages();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    SupportedCultures = new List<CultureInfo>
    {
        new("en-GB")
    }
});
app.Run();
