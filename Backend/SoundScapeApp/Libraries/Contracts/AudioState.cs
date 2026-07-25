namespace SoundScapeApp.Libraries.Contracts;

public class AudioState(
bool isActive = false,
string? selectedConfigId = null,
string? configName = null,
string? inputDeviceId = null,
string? outputDeviceId = null,
List<string>? activeFilterIds = null)
{
    public bool IsActive { get; private set; } = isActive;
    public string? SelectedConfigId { get; private set; } = selectedConfigId;
    public string? ConfigName { get; private set; } = configName;
    public string? InputDeviceId { get; private set; } = inputDeviceId;
    public string? OutputDeviceId { get; private set; } = outputDeviceId;
    public List<string> ActiveFilterIds { get; private set; } = activeFilterIds ?? [];
}

public class AudioStateUpdateRequest
{
    public bool IsActive { get; set; }

    public CustomMicConfiguration Config { get; set; } = default!;
}
