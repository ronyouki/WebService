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

    CosmosTodoApi.Services.CosmosDbService db;
    bool init = false;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string value1)
    {
        if(value1!=null){
            OpenAIApp.Program.SetQuestion(value1);
        }
        OpenAIApp.Program.OpenAIMain(null).GetAwaiter().GetResult();
        ConsoleApplication1.ProgramWeather.Run(null);

        if(!init){
            db = CosmosTodoApi.Services.CosmosDbService.InitializeCosmosClientInstanceAsync().GetAwaiter().GetResult();
        }
        if(value1!=null){
            CosmosTodoApi.Models.QAItem item = new CosmosTodoApi.Models.QAItem{ 
                Id=Guid.NewGuid().ToString(), 
                Date=DateTime.Now,
                Question=OpenAIApp.Program.GetQuestion(), 
                Answer=OpenAIApp.Program.GetResult()
            };
            
            db.AddItemAsync(item).GetAwaiter().GetResult();
            CreatedAtAction("Get", new { id = item.Id }, item);
        }
        db.GetItemsAsync("SELECT * FROM c ORDER BY c.date DESC OFFSET 0 LIMIT 20").GetAwaiter().GetResult();

        return View(new MessageViewModel{
            Weather=ConsoleApplication1.ProgramWeather.GetResult(),
            OpenAIAnswer=OpenAIApp.Program.GetResult(),
            Items = db.GetItems().ToList()
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
