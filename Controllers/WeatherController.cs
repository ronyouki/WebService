using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMVCapp.Models;

namespace MyMVCapp.Controllers;

public class WeatherController : Controller
{
    private readonly ILogger<WeatherController> _logger;

    public WeatherController(ILogger<WeatherController> logger)
    {
        _logger = logger;
    }

    public string Index()
    {
        return "Weather";
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
