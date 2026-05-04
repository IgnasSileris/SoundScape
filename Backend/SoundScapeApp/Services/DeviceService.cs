using PortAudioSharp;

using SoundScapeApp.Libraries.Contracts;
using SoundScapeApp.Libraries.Utilities;

namespace SoundScapeApp.Services;

public class DeviceService
{
    private readonly AudioStateService state;

    public DeviceService(AudioStateService _state)
    {
        state = _state;

        PortAudio.Initialize();
    }

    public List<DeviceOptionDto> GetInputMicOptions()
    {
        List<DeviceOption> devices = [];
        for (int i = 0; i < PortAudio.DeviceCount; i++)
        {
            var deviceInfo = PortAudio.GetDeviceInfo(i);

            bool isVirtualMic = Constants.dummyDeviceNames.Any(deviceInfo.name.Contains);

            if (deviceInfo.maxInputChannels > 0 && deviceInfo.hostApi == 1 && !isVirtualMic)
            {
                devices.Add(
                    new DeviceOption
                    {
                        Id = $"{deviceInfo.hostApi}:{deviceInfo.name}:{deviceInfo.defaultSampleRate}",
                        Name = $"{deviceInfo.name}",
                        PortAudioIndex = i
                    });
            }
        }

        state.SetInputDevices(devices);

        return [.. devices.Select(d => new DeviceOptionDto
        {
            Id = d.Id,
            Name = d.Name
        })];
    }

    public List<DeviceOptionDto> GetOutputMicOptions()
    {
        List<DeviceOption> devices = [];
        for (int i = 0; i < PortAudio.DeviceCount; i++)
        {
            var deviceInfo = PortAudio.GetDeviceInfo(i);

            bool isVirtualMic = Constants.virtualMicNames.Any(deviceInfo.name.Contains);

            if (deviceInfo.maxOutputChannels > 0 && deviceInfo.hostApi == 1 && isVirtualMic)
            {
                devices.Add(
                    new DeviceOption
                    {
                        Id = $"{deviceInfo.hostApi}:{deviceInfo.name}:{deviceInfo.defaultSampleRate}",
                        Name = $"{deviceInfo.name}",
                        PortAudioIndex = i
                    });
            }
        }

        state.SetOutputDevices(devices);

        return [.. devices.Select(d => new DeviceOptionDto
        {
            Id = d.Id,
            Name = d.Name
        })];
    }
}
