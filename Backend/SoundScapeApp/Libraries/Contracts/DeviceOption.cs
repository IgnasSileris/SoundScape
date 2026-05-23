using System.Text.Json.Serialization;

namespace SoundScapeApp.Libraries.Contracts;

public class DeviceOption
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int PortAudioIndex { get; set; }
}


public class DeviceOptionDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}
