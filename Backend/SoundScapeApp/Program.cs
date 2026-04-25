using ElectronNET.API;

using SoundScapeApp.Electron;
using SoundScapeApp.Services;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();
builder.WebHost.UseElectron(args);

var app = builder.Build();

ElectronBootstrap.Init("http://localhost:5173");

app.UseRouting();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}")
//     .WithStaticAssets();

app.Run();
