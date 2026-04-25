namespace SoundScapeApp.Libraries.Contracts;

public class DeviceOption
{
    public string Id { get; set; } = default!;
    public string Label { get; set; } = default!;
    public int PortAudioIndex { get; set; }
}


public class DeviceOptionDto
{
    public string Id { get; set; } = default!;
    public string Label { get; set; } = default!;
}
