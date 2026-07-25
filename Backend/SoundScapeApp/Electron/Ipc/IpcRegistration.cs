namespace SoundScapeApp.Electron.Ipc;

public class IpcRegistration(DeviceHandler _deviceHandler, ConfigHandler _configHandler, AudioStateHandler _audioStateHandler)
{
    private readonly DeviceHandler deviceHandler = _deviceHandler;
    private readonly ConfigHandler configHandler = _configHandler;
    private readonly AudioStateHandler audioStateHandler = _audioStateHandler;

    public void RegisterIpc()
    {
        deviceHandler.Register();
        configHandler.Register();
        audioStateHandler.Register();
        return;
    }
}