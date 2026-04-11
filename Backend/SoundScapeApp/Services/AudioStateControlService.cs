namespace SoundScapeApp.Services;

public class AudioStateControlService
{
    private readonly AudioStateService state;

    public AudioStateControlService(AudioStateService _state)
    {
        state = _state;
    }

    public void UpdateDeviceState(bool newIsActive, string newCustomName, string newInputDeviceId, string newOutputDeviceId, List<string> newActiveFilterIds)
    {
        UpdateIsActiveStatus(newIsActive);
        UpdateDeviceCustomName(newCustomName);
        UpdateInputDeviceId(newInputDeviceId);
        UpdateOutputDeviceId(newOutputDeviceId);
        UpdateActiveFilterIds(newActiveFilterIds);
    }

    public void UpdateIsActiveStatus(bool newIsActive)
    {
        state.IsActive = newIsActive;
    }

    public void UpdateDeviceCustomName(string newCustomName)
    {
        state.CustomDeviceName = newCustomName;
    }

    public void UpdateInputDeviceId(string newInputDeviceId)
    {
        state.InputDeviceId = newInputDeviceId;
    }

    public void UpdateOutputDeviceId(string newOutputDeviceId)
    {
        state.OutputDeviceId = newOutputDeviceId;
    }

    public void UpdateActiveFilterIds(List<string> newActiveFilterIds)
    {
        state.ActiveFilterIds = newActiveFilterIds;
    }
}