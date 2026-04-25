using ElectronNET;
using ElectronNET.API;
using ElectronNET.API.Entities;
using SoundScapeApp.Services;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();
builder.Services.AddElectron();
builder.UseElectron(
    args,
    async () =>
    {
        SoundScapeApp.Electron.Ipc.IpcRegistration.RegisterIpc();

        var browserWindow = await Electron.WindowManager.CreateWindowAsync(
            new BrowserWindowOptions
            {
                Width = 1200,
                Height = 800,
                Show = true,
                WebPreferences = new WebPreferences
                {
                    ContextIsolation = false,
                    NodeIntegration = false,
                    WebSecurity = false,
                    AllowRunningInsecureContent = true
                }
            },
            "http://localhost:5173"
        );

        browserWindow.OnReadyToShow += () => browserWindow.Show();
    }
);

var app = builder.Build();

app.UseRouting();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}")
//     .WithStaticAssets();

app.Run();
