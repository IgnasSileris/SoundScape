using SoundScapeApp.Libraries.Contracts;
using SoundScapeApp.Services;

namespace SoundScapeApp.Electron.Ipc;

public class AudioStateHandler(AudioStateApplicationService _audioStateApplicationService)
{
    private readonly AudioStateApplicationService audioStateApplicationService = _audioStateApplicationService;

    public void Register()
    {
        ElectronNET.API.Electron.IpcMain.Handle("audio-state:update", payload =>
        {
            var updateRequest = IpcJson.Deserialize<AudioStateUpdateRequest>(payload);

            if (updateRequest == null)
            {
                return false;
            }

            return audioStateApplicationService.ProcessStateChange(updateRequest.IsActive, updateRequest.Config);
        });
    }
}
