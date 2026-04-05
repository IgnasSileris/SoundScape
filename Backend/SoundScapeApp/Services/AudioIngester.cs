using System.Runtime.Versioning;
using PortAudioSharp;

namespace SoundScapeApp.Services;

public class AudioIngester
{
    private CircularBuffer _circularBuffer;
    private readonly AudioStateService state;

    public AudioIngester(AudioStateService _state)
    {
        _circularBuffer = new CircularBuffer();
        state = _state;

        PortAudio.Initialize();
    }

}