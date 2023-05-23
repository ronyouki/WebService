using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMVCapp.Models;

namespace MyMVCapp.Controllers;

/*
public class ControllerParam{
    public static Message controllerMessage;

    public static void SetControllerMessage(Message arg)
    {
        controllerMessage = arg;
    }
}

*/

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;



    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index()
    {
        OpenAIApp.Program.OpenAIMain(null).GetAwaiter().GetResult();
        ConsoleApplication1.ProgramWeather.Run(null);
        return View(new MessageViewModel{
            Weather=ConsoleApplication1.ProgramWeather.GetResult(),
            OpenAIAnswer=OpenAIApp.Program.GetResult()
        });
    }

    public IActionResult Weather()
    {
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
