namespace SoundScapeApp.Electron.Ipc;

public class IpcRegistration(DeviceHandler _deviceHandler)
{
    private readonly DeviceHandler deviceHandler = _deviceHandler;
    public void RegisterIpc()
    {
        deviceHandler.Register();
        return;
    }
}