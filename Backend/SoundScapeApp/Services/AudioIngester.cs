using PortAudioSharp;

namespace SoundScapeApp.Services;

public class AudioIngester
{
    private CircularBuffer circularBuffer;
    private readonly AudioStateService state;

    public AudioIngester(AudioStateService _state, CircularBuffer _buffer)
    {
        circularBuffer = _buffer;
        state = _state;

        PortAudio.Initialize();
    }

}