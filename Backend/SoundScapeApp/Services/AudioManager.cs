using System;
using Microsoft.Extensions.Hosting;
using SoundScapeApp.Libraries.Filters;

namespace SoundScapeApp.Services;

public class AudioManager(AudioStateService _state, CircularBuffer<float> _inputBuffer, CircularBuffer<float> _outputBuffer, FilterRegistry _filterRegistry) : BackgroundService
{
    private readonly AudioStateService state = _state;
    private readonly CircularBuffer<float> inputBuffer = _inputBuffer;
    private readonly CircularBuffer<float> outputBuffer = _outputBuffer;
    private readonly FilterRegistry filterRegistry = _filterRegistry;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        state.OnActiveFiltersChanged += OnActiveFiltersChangedHandler;

        try
        {
            Span<float> rawAudioChunk = new float[32];

            int readBytes = inputBuffer.BulkRead(rawAudioChunk);
            var processedAudio = ApplyFilters(rawAudioChunk);
            int writtenBytes = outputBuffer.BulkWrite(processedAudio);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        finally
        {
            state.OnActiveFiltersChanged -= OnActiveFiltersChangedHandler;
        }
    }

    private Span<float> ApplyFilters(Span<float> rawAudioChunk)
    {
        foreach (string id in state.ActiveFilterIds)
        {
            var filter = filterRegistry.GetFilter(id);

            if (filter == null)
            {
                continue;
            }

            // TODO: call filter function here
        }

        return rawAudioChunk;
    }


    private void OnActiveFiltersChangedHandler(List<string> activeFilterIds)
    {
        return;
    }


}