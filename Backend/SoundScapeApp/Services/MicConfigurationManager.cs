using SoundScapeApp.Libraries.Contracts;
using SoundScapeApp.Electron.Ipc;
using System.Text.Json;

namespace SoundScapeApp.Services;

public class MicConfigurationManager
{
    private readonly string configFilePath = "";

    public MicConfigurationManager()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var soundScapeFolderPath = Path.Combine(appDataPath, "SoundScape");

        Directory.CreateDirectory(soundScapeFolderPath);

        configFilePath = Path.Combine(soundScapeFolderPath, "SavedMicConfigurations.json");
    }

    public bool SaveConfig(CustomMicConfiguration config)
    {
        if (!config.ContainsPersistableFields())
        {
            return false;
        }

        List<CustomMicConfiguration> existingConfigs = GetConfigs();
        var existingIndex = existingConfigs.FindIndex(c => c.Id == config.Id);

        if (existingIndex == -1)
        {
            existingConfigs.Add(config);
        }
        else
        {
            existingConfigs[existingIndex] = config;
        }

        return TrySaveJson(existingConfigs);
    }

    public bool DeleteConfig(string configId)
    {
        List<CustomMicConfiguration> existingConfigs = GetConfigs();
        var existingIndex = existingConfigs.FindIndex(c => c.Id == configId);

        if (existingIndex == -1)
        {
            return false;
        }

        existingConfigs.RemoveAt(existingIndex);

        return TrySaveJson(existingConfigs);
    }

    public List<CustomMicConfiguration> GetConfigs()
    {
        if (!File.Exists(configFilePath))
        {
            return [];
        }

        try
        {
            var json = File.ReadAllText(configFilePath);
            var micConfigs = IpcJson.Deserialize<List<CustomMicConfiguration>>(json);
            return micConfigs ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading from saved configurations json: {e}");
            return [];
        }
    }

    private bool TrySaveJson<T>(T serializable)
    {
        try
        {
            var json = IpcJson.Serialize(serializable);
            File.WriteAllText(configFilePath, json);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error writing to saved configurations json: {e} ");

            return false;
        }
    }
}