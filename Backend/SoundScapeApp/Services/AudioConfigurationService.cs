namespace SoundScapeApp.Services;

public class AudioConfigurationService
{
    private readonly AudioStateService state;

    public AudioConfigurationService(AudioStateService _state)
    {
        state = _state;
    }
    // TODO: update active state vs update existing state
    // TODO: better approach: receive IsActiveStatus and ActiveCustomId status and then update active state based on Config manager. 
    public void UpdateDeviceState(bool newIsActive, string newCustomId, string newCustomName, string newInputDeviceId, string newOutputDeviceId, List<string> newActiveFilterIds)
    {
        UpdateIsActiveStatus(newIsActive);
        UpdateCustomId(newCustomId);
        UpdateDeviceCustomName(newCustomName);
        UpdateInputDeviceId(newInputDeviceId);
        UpdateOutputDeviceId(newOutputDeviceId);
        UpdateActiveFilterIds(newActiveFilterIds);
    }

    public void UpdateIsActiveStatus(bool newIsActive)
    {
        state.IsActive = newIsActive;
    }

    public void UpdateCustomId(string newCustomId)
    {
        state.CustomId = newCustomId;
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