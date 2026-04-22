using ElectronNET.API;

using SoundScapeApp.Electron;
using SoundScapeApp.Services;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();

var app = builder.Build();

if (HybridSupport.IsElectronActive)
{
    ElectronBootstrap.Init("http://localhost:5173");
}

app.UseRouting();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}")
//     .WithStaticAssets();

app.Run();
