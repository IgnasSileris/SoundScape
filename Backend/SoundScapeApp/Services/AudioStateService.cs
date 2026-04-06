using SoundScapeApp.Libraries.Contracts;

namespace SoundScapeApp.Services;

public class AudioStateService
{
    public event Action<string?>? OnInputDeviceChanged;
    public event Action<string?>? OnOutputDeviceChanged;
    public event Action<List<string>>? OnActiveFiltersChanged;

    public string? CustomDeviceName
    {
        get;
        set
        {
            if (field == value)
            {
                return;
            }

            field = value;
        }
    }

    public string? InputDeviceId
    {
        get;
        set
        {
            if (field == value)
            {
                return;
            }

            field = value;
            OnInputDeviceChanged?.Invoke(value);
        }
    }

    public string? OutputDeviceId
    {
        get;
        set
        {
            if (field == value)
            {
                return;
            }

            field = value;
            OnOutputDeviceChanged?.Invoke(value);
        }
    }

    public List<string> ActiveFilterIds
    {
        get;
        set
        {
            if (field.SequenceEqual(value))
            {
                return;
            }

            field = value;
            OnActiveFiltersChanged?.Invoke(value);
        }
    } = [];

    public IReadOnlyList<DeviceOption> AvailableInputDevices { get; private set; } = [];
    public IReadOnlyList<DeviceOption> AvailableOutputDevices { get; private set; } = [];

    public void SetInputDevices(List<DeviceOption> _inputDevices)
    {
        AvailableInputDevices = _inputDevices;
    }

    public void SetOutputDevices(List<DeviceOption> _outputDevices)
    {
        AvailableOutputDevices = _outputDevices;
    }

}