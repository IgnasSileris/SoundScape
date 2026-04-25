using ElectronNET.API;
using ElectronNET.API.Entities;

namespace SoundScapeApp.Electron;

public static class ElectronBootstrap
{
    public static void Init(string url)
    {
        ElectronNET.API.Electron.App.Ready += async () =>
        {
            Ipc.IpcRegistration.RegisterIpc();

            await ElectronNET.API.Electron.WindowManager.CreateWindowAsync(
                new BrowserWindowOptions
                {
                    Width = 1200,
                    Height = 800,
                    Show = true,
                },
                url
            );
        };
    }
}