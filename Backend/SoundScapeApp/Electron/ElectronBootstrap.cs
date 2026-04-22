namespace SoundScapeApp.Electron;

public static class ElectronBootstrap
{
    public static void Init(string url)
    {
        ElectronNET.API.Electron.App.Ready += () =>
        {
            Task.Run(async () =>
            {
                Ipc.IpcRegistration.RegisterIpc();

                await ElectronNET.API.Electron.WindowManager.CreateWindowAsync(
                    new ElectronNET.API.Entities.BrowserWindowOptions
                    {
                        Width = 1200,
                        Height = 800
                    },
                    url
                );
            });
        };
    }
}
