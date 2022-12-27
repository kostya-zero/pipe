using System.Text.Json;
using Pipe.Models;

namespace Pipe.Utils;

public class Configs
{
    public static bool CheckForConfig()
    {
        return File.Exists("project.pipe");
    }

    public static void MakeConfig(BuildConfigModel config)
    {
        string json = JsonSerializer.Serialize(config, new JsonSerializerOptions{ WriteIndented = true });
        File.Create("project.pipe").Close();
        File.WriteAllText("project.pipe", json);
    }

    public static BuildConfigModel GetConfig()
    {
        string config = File.ReadAllText("project.pipe");
        var configModel = JsonSerializer.Deserialize<BuildConfigModel>(config);
        return configModel;
    }
}