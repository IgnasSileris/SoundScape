using SoundScapeApp.Libraries.Contracts;

namespace SoundScapeApp.Services;

public class MicConfigurationManager
{
    public bool SaveConfig(CustomMicConfiguration config)
    {
        return true;
    }

    public bool DeleteConfig(string configId)
    {
        return true;
    }

    public List<CustomMicConfiguration> GetConfigs()
    {
        return [];
    }
}