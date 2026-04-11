using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;
using System.Threading;
using PortAudioSharp;

namespace SoundScapeApp.Services;

public class AudioIngestionService(AudioStateService _state, CircularBuffer _buffer) : BackgroundService
{
    private readonly CircularBuffer circularBuffer = _buffer;
    private readonly AudioStateService state = _state;

    private PortAudioSharp.Stream? stream;
    private Lock _lock = new();


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        PortAudio.Initialize();

        // TODO: event ordering logic (double starting, double stopping)
        state.OnIsActiveChanged += OnIsActiveChangedHandler;
        state.OnInputDeviceChanged += OnInputDeviceChangedHandler;

        try
        {
            if (state.IsActive)
            {
                StartStream();
            }

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        finally
        {
            state.OnIsActiveChanged -= OnIsActiveChangedHandler;
            state.OnInputDeviceChanged -= OnInputDeviceChangedHandler;

            StopStream();
            PortAudio.Terminate();
        }
    }

    private void StartStream()
    {
        StopStream();

        int deviceIx = state.GetInputPortAudioIndex();

        if (deviceIx < 0)
        {
            throw new Exception("No input devices.");
        }

        lock (_lock)
        {
            var param = new StreamParameters
            {
                channelCount = 1,
                sampleFormat = SampleFormat.Float32,
                suggestedLatency = PortAudio.GetDeviceInfo(deviceIx).defaultHighInputLatency,
                hostApiSpecificStreamInfo = IntPtr.Zero
            };

            stream = new PortAudioSharp.Stream(
                inParams: param,
                outParams: null,
                sampleRate: 44100,
                framesPerBuffer: 0,
                streamFlags: StreamFlags.ClipOff,
                callback: Callback,
                userData: IntPtr.Zero
            );
            stream.Start();
        }

        Console.WriteLine($"Started new audio stream with {PortAudio.GetDeviceInfo(deviceIx).name}.");
    }

    private void StopStream()
    {
        lock (_lock)
        {
            if (stream == null)
            {
                return;
            }
            stream.Stop();
            stream.Dispose();
            stream = null;
        }
    }

    private void OnIsActiveChangedHandler(bool isActive)
    {
        if (isActive)
        {
            StartStream();
        }
        else
        {
            StopStream();
        }

    }

    private void OnInputDeviceChangedHandler(string? inputDeviceId)
    {
        if (state.IsActive)
        {
            StartStream();
        }
    }

    static StreamCallbackResult Callback(nint input, nint output, uint frameCount, ref StreamCallbackTimeInfo timeInfo, StreamCallbackFlags statusFlags, nint userData)
    {
        var samples = new float[frameCount];
        Marshal.Copy(input, samples, 0, (int)frameCount);

        return StreamCallbackResult.Continue;
    }

}