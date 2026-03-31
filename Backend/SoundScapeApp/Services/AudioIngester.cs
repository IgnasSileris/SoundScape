using PortAudioSharp;

namespace SoundScapeApp.Services;

static class Constants
{
    public static readonly string[] virtualDeviceNames = ["Stereo Mix", "Sound Mapper", "Primary Sound Capture Driver", "Monitor", "Loopback", "Dummy", "Virtual"];
}

public class AudioIngester
{
    private CircularBuffer _circularBuffer;

    public AudioIngester()
    {
        _circularBuffer = new CircularBuffer();

        PortAudio.Initialize();
    }

    public List<string> GetMicOptions()
    {
        List<string> micNames = [];
        for (int i = 0; i < PortAudio.DeviceCount; i++)
        {
            var deviceInfo = PortAudio.GetDeviceInfo(i);

            bool isVirtualMic = Constants.virtualDeviceNames.Any(deviceInfo.name.Contains);

            if (deviceInfo.maxInputChannels > 0 && deviceInfo.hostApi == 1 && !isVirtualMic)
            {
                micNames.Add(deviceInfo.name);
            }
        }

        return micNames;
    }
}