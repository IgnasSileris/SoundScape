namespace SoundScapeApp.Libraries.Contracts;

public class CustomMicConfiguration
{
    public string Id { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string InputDeviceId { get; set; } = default!;

    public string OutputDeviceId { get; set; } = default!;

    public bool ReduceBackgroundNoise { get; set; } = false;

    public string? FilterId { get; set; } = default!;

    public bool ContainsRunnableFields()
    {
        return !string.IsNullOrWhiteSpace(InputDeviceId) && !string.IsNullOrWhiteSpace(OutputDeviceId);
    }

    public bool ContainsPersistableFields()
    {
        return !string.IsNullOrWhiteSpace(Id) && !string.IsNullOrWhiteSpace(Name) && ContainsRunnableFields();
    }
}
