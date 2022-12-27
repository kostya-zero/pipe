using System.Text.Json;
using Pipe.Models;

namespace Pipe.Utils;

public class Configs
{
    public static bool CheckForConfig()
    {
        return File.Exists("pipe.project");
    }

    public static void MakeConfig(BuildConfigModel config)
    {
        string json = JsonSerializer.Serialize(config, new JsonSerializerOptions{ WriteIndented = true });
        File.Create("pipe.project").Close();
        File.WriteAllText("pipe.project", json);
    }

    public static BuildConfigModel GetConfig()
    {
        string config = File.ReadAllText("pipe.project");
        var configModel = JsonSerializer.Deserialize<BuildConfigModel>(config);
        return configModel;
    }
}