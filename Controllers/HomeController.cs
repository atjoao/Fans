using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fans.Models;
using Fans.Attributes;
using System.Text.Json;

namespace Fans.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HasSession]
    public IActionResult Index()
    {
        var user = HttpContext.Session.GetString("user");
        if (user != null)
        {
            var json_user = JsonSerializer.Deserialize<User>(user);
            ViewData["user"] = json_user;
        }
        return View();

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
