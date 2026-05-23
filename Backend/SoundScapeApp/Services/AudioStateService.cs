using SoundScapeApp.Libraries.Contracts;

namespace SoundScapeApp.Services;

public class AudioStateService
{
    public event Action<bool>? OnIsActiveChanged;
    public event Action<string?>? OnInputDeviceChanged;
    public event Action<string?>? OnOutputDeviceChanged;
    public event Action<List<string>>? OnActiveFiltersChanged;

    private AudioState coreState = new();

    public bool IsActive => coreState.IsActive;
    public string? SelectedConfigId => coreState.SelectedConfigId;
    public string? ConfigName => coreState.ConfigName;
    public string? InputDeviceId => coreState.InputDeviceId;
    public string? OutputDeviceId => coreState.OutputDeviceId;
    public List<string> ActiveFilterIds => coreState.ActiveFilterIds;

    public IReadOnlyList<DeviceOption> AvailableInputDevices { get; private set; } = [];
    public IReadOnlyList<DeviceOption> AvailableOutputDevices { get; private set; } = [];

    public void SetCoreState(AudioState newCoreState)
    {
        bool isActiveChanged = coreState.IsActive != newCoreState.IsActive;
        bool isInputDeviceChanged = coreState.InputDeviceId != newCoreState.InputDeviceId;
        bool isOutputDeviceChanged = coreState.OutputDeviceId != newCoreState.OutputDeviceId;
        bool isActiveFiltersChanged = !coreState.ActiveFilterIds.SequenceEqual(newCoreState.ActiveFilterIds);

        coreState = newCoreState;

        if (isActiveChanged)
        {
            OnIsActiveChanged?.Invoke(coreState.IsActive);
        }
        if (isInputDeviceChanged)
        {
            OnInputDeviceChanged?.Invoke(coreState.InputDeviceId);
        }
        if (isOutputDeviceChanged)
        {
            OnOutputDeviceChanged?.Invoke(coreState.OutputDeviceId);
        }
        if (isActiveFiltersChanged)
        {
            OnActiveFiltersChanged?.Invoke(coreState.ActiveFilterIds);
        }

    }

    public void SetInputDevices(List<DeviceOption> _inputDevices)
    {
        AvailableInputDevices = _inputDevices;
    }

    public void SetOutputDevices(List<DeviceOption> _outputDevices)
    {
        AvailableOutputDevices = _outputDevices;
    }

    public int GetInputPortAudioIndex()
    {
        var device = AvailableInputDevices.FirstOrDefault(d => d.Id == InputDeviceId);

        if (device == null)
        {
            return -1;
        }

        return device.PortAudioIndex;
    }

    public int GetOutputPortAudioIndex()
    {
        var device = AvailableOutputDevices.FirstOrDefault(d => d.Id == OutputDeviceId);

        if (device == null)
        {
            return -1;
        }

        return device.PortAudioIndex;
    }

}