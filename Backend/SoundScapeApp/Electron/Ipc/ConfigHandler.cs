using SoundScapeApp.Libraries.Contracts;
using SoundScapeApp.Services;

namespace SoundScapeApp.Electron.Ipc;

public class ConfigHandler(MicConfigurationManager _configurationManager)
{
    private readonly MicConfigurationManager configurationManager = _configurationManager;

    public void Register()
    {
        ElectronNET.API.Electron.IpcMain.Handle("config:get-all", _ =>
        {
            return configurationManager.GetConfigs();
        });

        ElectronNET.API.Electron.IpcMain.Handle("config:save", payload =>
        {
            var config = IpcJson.Deserialize<CustomMicConfiguration>(payload);

            if (config == null)
            {
                return false;
            }

            return configurationManager.SaveConfig(config);
        });

        ElectronNET.API.Electron.IpcMain.Handle("config:delete", payload =>
        {
            var configId = payload.ToString();

            if (string.IsNullOrWhiteSpace(configId))
            {
                return false;
            }

            return configurationManager.DeleteConfig(configId);
        });
    }
}
