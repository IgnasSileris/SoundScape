namespace SoundScapeApp.Services;

public class AudioStateService
{
    public string? CustomMicName { get; set; }
    public string? InputDeviceId { get; set; }
    public string? OutputDeviceId { get; set; }

    public List<string> ActiveFilterIds { get; } = [];

}