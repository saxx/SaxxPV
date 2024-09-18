using Microsoft.AspNetCore.Mvc;
using SaxxPv.Web.ViewModels.Home;

namespace SaxxPv.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction(nameof(Day));
    }

    public async Task<IActionResult> Day([FromServices] DayViewModelFactory factory, DateOnly? day)
    {
        day ??= new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
        return View(await factory.Build(day.Value));
    }

    public async Task<IActionResult> Month([FromServices] MonthViewModelFactory factory, DateOnly? day)
    {
        day ??= new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
        return View(await factory.Build(day.Value));
    }
}
