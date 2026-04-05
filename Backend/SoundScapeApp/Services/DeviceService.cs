using PortAudioSharp;

using SoundScapeApp.Utilities;
using SoundScapeApp.Libraries.Contracts;

namespace SoundScapeApp.Services;

public class DeviceService
{
    public List<DeviceOptionDto> GetInputMicOptions()
    {
        List<DeviceOptionDto> micOptions = [];
        for (int i = 0; i < PortAudio.DeviceCount; i++)
        {
            var deviceInfo = PortAudio.GetDeviceInfo(i);

            bool isVirtualMic = Constants.dummyDeviceNames.Any(deviceInfo.name.Contains);

            if (deviceInfo.maxInputChannels > 0 && deviceInfo.hostApi == 1 && !isVirtualMic)
            {
                micOptions.Add(
                    new DeviceOptionDto
                    {
                        Id = $"{deviceInfo.hostApi}:{deviceInfo.name}   ",
                        Label = $"{deviceInfo.name}"
                    });
            }
        }

        return micOptions;
    }

    public List<DeviceOptionDto> GetOutputMicOptions()
    {
        List<DeviceOptionDto> micOptions = [];
        for (int i = 0; i < PortAudio.DeviceCount; i++)
        {
            var deviceInfo = PortAudio.GetDeviceInfo(i);

            bool isVirtualMic = Constants.virtualMicNames.Any(deviceInfo.name.Contains);

            if (deviceInfo.maxInputChannels > 0 && deviceInfo.hostApi == 1 && isVirtualMic)
            {
                micOptions.Add(
                    new DeviceOptionDto
                    {
                        Id = $"{deviceInfo.hostApi}:{deviceInfo.name}   ",
                        Label = $"{deviceInfo.name}"
                    });
            }
        }

        return micOptions;
    }
}
