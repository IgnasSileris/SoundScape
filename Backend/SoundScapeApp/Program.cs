using ElectronNET;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.Extensions.DependencyInjection;
using SoundScapeApp.Services;
using SoundScapeApp.Electron.Ipc;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();

//Services
builder.Services.AddSingleton<DeviceService>();

// IPC handlers
builder.Services.AddSingleton<IpcRegistration>();
builder.Services.AddSingleton<DeviceHandler>();
builder.Services.AddSingleton<ConfigHandler>();

builder.Services.AddElectron();

var app = builder.Build();

builder.UseElectron(
    args,
    async () =>
    {
        var ipcRegistration = app.Services.GetRequiredService<IpcRegistration>();
        ipcRegistration.RegisterIpc();

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



app.UseRouting();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}")
//     .WithStaticAssets();

app.Run();
