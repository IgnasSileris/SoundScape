using SoundScapeApp.Libraries.Utilities;

using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;
using System.Threading;
using PortAudioSharp;

namespace SoundScapeApp.Services;

public class VirtualAudioService(AudioStateService _state, CircularBuffer<float> _inputBuffer, CircularBuffer<float> _outputBuffer) : BackgroundService
{
    private readonly CircularBuffer<float> inputBuffer = _inputBuffer;
    private readonly CircularBuffer<float> outputBuffer = _outputBuffer;
    private readonly AudioStateService state = _state;

    private PortAudioSharp.Stream? stream;
    private readonly Lock _lock = new();
    private Action? debouncedEvaluateState;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        PortAudio.Initialize();

        state.OnIsActiveChanged += OnIsActiveChangedHandler;
        state.OnInputDeviceChanged += OnInputDeviceChangedHandler;
        state.OnOutputDeviceChanged += OnOutputDeviceChangedHandler;
        debouncedEvaluateState = Debouncer.Debounce(EvaluateState, 500);

        try
        {
            EvaluateState();

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        finally
        {
            state.OnIsActiveChanged -= OnIsActiveChangedHandler;
            state.OnInputDeviceChanged -= OnInputDeviceChangedHandler;
            state.OnOutputDeviceChanged -= OnOutputDeviceChangedHandler;

            StopStream();
            PortAudio.Terminate();
        }
    }

    private void StartStream()
    {
        int inputDeviceIx = state.GetInputPortAudioIndex();
        if (inputDeviceIx < 0)
        {
            // TODO: report back to FE or maybe just return
            return;
            throw new Exception("No input devices.");
        }

        int outputDeviceIx = state.GetOutputPortAudioIndex();
        if (outputDeviceIx < 0)
        {
            // TODO: report back to FE or maybe just return
            return;
            throw new Exception("No output devices.");
        }

        lock (_lock)
        {
            StopStream();

            var inputParams = new StreamParameters
            {
                channelCount = 1,
                sampleFormat = SampleFormat.Float32,
                suggestedLatency = PortAudio.GetDeviceInfo(inputDeviceIx).defaultHighInputLatency,
                hostApiSpecificStreamInfo = IntPtr.Zero
            };

            var outputParams = new StreamParameters
            {
                channelCount = 1,
                sampleFormat = SampleFormat.Float32,
                suggestedLatency = PortAudio.GetDeviceInfo(outputDeviceIx).defaultHighInputLatency,
                hostApiSpecificStreamInfo = IntPtr.Zero
            };

            stream = new PortAudioSharp.Stream(
                inParams: inputParams,
                outParams: outputParams,
                sampleRate: 44100,
                framesPerBuffer: 0,
                streamFlags: StreamFlags.ClipOff,
                callback: Callback,
                userData: IntPtr.Zero
            );
            stream.Start();
        }

        Console.WriteLine($"Started new audio stream with input from: {PortAudio.GetDeviceInfo(inputDeviceIx).name} and output to {PortAudio.GetDeviceInfo(outputDeviceIx).name}.");
    }

    private unsafe StreamCallbackResult Callback(nint input, nint output, uint frameCount, ref StreamCallbackTimeInfo timeInfo, StreamCallbackFlags statusFlags, nint userData)
    {
        if (input != IntPtr.Zero && output != IntPtr.Zero)
        {
            var inputSpan = new Span<float>((float*)input, (int)frameCount);
            int numWrite = inputBuffer.BulkWrite(inputSpan);
            // * Async processing in the background with AudioManager * 
            var outputSpan = new Span<float>((float*)output, (int)frameCount);
            int numRead = outputBuffer.BulkRead(outputSpan);
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
        debouncedEvaluateState?.Invoke();
    }

    private void OnInputDeviceChangedHandler(string? inputDeviceId)
    {
        debouncedEvaluateState?.Invoke();
    }

    private void OnOutputDeviceChangedHandler(string? inputDeviceId)
    {
        debouncedEvaluateState?.Invoke();
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