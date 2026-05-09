namespace SoundScapeApp.Libraries.Contracts;

public class CustomMicConfiguration
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string InputMicId { get; set; } = default!;
    public string OutputMidId { get; set; } = default!;
    public bool ReduceBackgroundNoise { get; set; } = false;
    public string? FilterId { get; set; } = default!;
}
