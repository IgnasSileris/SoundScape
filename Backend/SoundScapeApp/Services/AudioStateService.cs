using SoundScapeApp.Libraries.Contracts;

namespace SoundScapeApp.Services;

public class AudioStateService
{
    public string? CustomDeviceName { get; set; }
    public string? InputDeviceId { get; set; }
    public string? OutputDeviceId { get; set; }

    public List<string> ActiveFilterIds { get; set; } = [];

    private List<DeviceOption> inputDevices = [];
    private List<DeviceOption> outputDevices = [];
    public IReadOnlyList<DeviceOption> AvailableInputDevices => inputDevices;
    public IReadOnlyList<DeviceOption> AvailableOutputDevices => outputDevices;

    public void SetInputDevices(List<DeviceOption> _inputDevices)
    {
        inputDevices = _inputDevices;
    }

    public void SetOutputDevices(List<DeviceOption> _outputDevices)
    {
        outputDevices = _outputDevices;
    }

}