using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fans.Models;
using Fans.Attributes;
using System.Text.Json;
using Fans.Data;

namespace Fans.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HasSession]
    public IActionResult Index()
    {
        // get posts
        var posts = _context.Posts.ToList();
        return View(posts);
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
