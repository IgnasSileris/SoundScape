using SoundScapeApp.Services;

namespace SoundScapeApp.Electron.Ipc;

public class DeviceHandler(DeviceService _deviceService)
{
    private readonly DeviceService deviceService = _deviceService;
    public void Register()
    {
        ElectronNET.API.Electron.IpcMain.Handle("devices:get-input-mics", _ =>
        {
            return deviceService.GetInputMicOptions();
        });

        ElectronNET.API.Electron.IpcMain.Handle("devices:get-output-mics", _ =>
        {
            return deviceService.GetOutputMicOptions();
        });
    }
}