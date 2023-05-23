using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//WeatherTest.WeatherClass weather = new WeatherTest.WeatherClass();
//weather.RunWeather();
//

//ControllerParam.SetControllerMessage(new Message() { message = "理科" });
        OpenAIApp.Program.OpenAIMain(null).GetAwaiter().GetResult();
        Console.WriteLine(OpenAIApp.Program.GetResult());
            
        

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
