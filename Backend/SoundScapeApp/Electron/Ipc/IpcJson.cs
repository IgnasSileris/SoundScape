using System.Text.Json;

namespace SoundScapeApp.Electron.Ipc;

public static class IpcJson
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    public static T? Deserialize<T>(object payload)
    {
        return JsonSerializer.Deserialize<T>(payload.ToString()!, Options);
    }
}