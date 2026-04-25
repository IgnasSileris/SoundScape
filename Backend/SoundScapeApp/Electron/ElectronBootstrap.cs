using ElectronNET.API;
using ElectronNET.API.Entities;

namespace SoundScapeApp.Electron;

public static class ElectronBootstrap
{
    public static void Init(string url)
    {
        Console.WriteLine("Registering Electron.App.Ready...");
        ElectronNET.API.Electron.App.Ready += async () =>
        {
            Console.WriteLine("Electron.App.Ready fired");
            Ipc.IpcRegistration.RegisterIpc();

            await ElectronNET.API.Electron.WindowManager.CreateWindowAsync(
                new BrowserWindowOptions
                {
                    Width = 1200,
                    Height = 800,
                    Show = true
                },
                url
            );
            Console.WriteLine("Window created");
        };
    }
}