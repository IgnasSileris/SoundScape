using SoundScapeApp.Libraries.Contracts;

namespace SoundScapeApp.Services;

public class AudioStateApplicationService(AudioStateService _state)
{
    private readonly AudioStateService state = _state;

    public bool ProcessStateChange(bool isActive, CustomMicConfiguration config)
    {
        if (!config.ContainsRunnableFields())
        {
            Console.WriteLine("Selected config is missing required fields.");
            return false;
        }

        var newState = MapConfigToState(isActive, config);
        state.SetCoreState(newState);
        return true;
    }

    private static AudioState MapConfigToState(bool isActive, CustomMicConfiguration config)
    {
        List<string> activeFilters = [];
        if (config.ReduceBackgroundNoise)
        {
            // Always run noise cancellation first
            activeFilters.Add("NoiseFilterId");
        }
        if (config.FilterId != null)
        {
            activeFilters.Add(config.FilterId);
        }

        return new AudioState(isActive, config.Id, config.Name, config.InputDeviceId, config.OutputDeviceId, activeFilters);
    }

}