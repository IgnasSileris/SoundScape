using System.Text.Json.Serialization;

namespace SoundScapeApp.Libraries.Contracts;

public class CustomMicConfiguration
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("inputDeviceId")]
    public string InputDeviceId { get; set; } = default!;

    [JsonPropertyName("outputDeviceId")]
    public string OutputDeviceId { get; set; } = default!;

    [JsonPropertyName("reduceBackgroundNoise")]
    public bool ReduceBackgroundNoise { get; set; } = false;

    [JsonPropertyName("filterId")]
    public string? FilterId { get; set; } = default!;
}
