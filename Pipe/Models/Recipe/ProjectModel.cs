using System.Text.Json.Serialization;

namespace Pipe.Models.Recipe;

public class ProjectModel
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = "pipe_recipe";
    
    [JsonPropertyName("Description")]
    public string Description { get; set; } = "An app built with Pipe build system!";
    
    [JsonPropertyName("MainExecutable")]
    public string MainExecutable { get; set; } = "main.py";
    
    [JsonPropertyName("Version")]
    public string Version { get; set; } = "0.1.0";
    
    [JsonPropertyName("Type")]
    public string Type { get; set; } = "app";
}