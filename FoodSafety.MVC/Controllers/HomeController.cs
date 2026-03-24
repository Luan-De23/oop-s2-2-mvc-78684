using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FoodSafety.MVC.Models;

namespace FoodSafety.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<FollowUpController> _logger;
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult AccessDenied()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogError("Unhandled exception occurred: {Message} by {User}");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}