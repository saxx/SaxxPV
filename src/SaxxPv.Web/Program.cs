using SaxxPv.Web.Models.Options;
using SaxxPv.Web.Services.Tables;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOptions<TablesOptions>("Tables");
builder.Services.AddTransient<TablesClient>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStatusCodePages();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();
app.Run();