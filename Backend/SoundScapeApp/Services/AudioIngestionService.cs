using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;
using System.Threading;
using PortAudioSharp;

namespace SoundScapeApp.Services;

public class AudioIngestionService(AudioStateService _state, CircularBuffer<float> _buffer) : BackgroundService
{
    private readonly CircularBuffer<float> circularBuffer = _buffer;
    private readonly AudioStateService state = _state;

    private PortAudioSharp.Stream? stream;
    private Lock _lock = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        PortAudio.Initialize();

        state.OnIsActiveChanged += OnIsActiveChangedHandler;
        state.OnInputDeviceChanged += OnInputDeviceChangedHandler;

        try
        {
            EvaluateState();

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
        int deviceIx = state.GetInputPortAudioIndex();

        if (deviceIx < 0)
        {
            // TODO: report back to FE or maybe just return
            return;
            throw new Exception("No input devices.");
        }

        lock (_lock)
        {
            StopStream();

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

    private unsafe StreamCallbackResult Callback(nint input, nint output, uint frameCount, ref StreamCallbackTimeInfo timeInfo, StreamCallbackFlags statusFlags, nint userData)
    {
        if (input != IntPtr.Zero)
        {
            var inputSpan = new Span<float>((float*)input, (int)frameCount);
            circularBuffer.BulkWrite(inputSpan);
        }

        return StreamCallbackResult.Continue;
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
        EvaluateState();
    }

    private void OnInputDeviceChangedHandler(string? inputDeviceId)
    {
        EvaluateState();
    }

    private void EvaluateState()
    {
        if (state.IsActive)
        {
            StartStream();
        }
        else
        {
            StopStream();
        }
    }
}