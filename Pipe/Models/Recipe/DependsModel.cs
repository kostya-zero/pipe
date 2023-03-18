using System.Text.Json.Serialization;

namespace Pipe.Models.Recipe;

public class DependsModel {

    [JsonPropertyName("Packages")] public List<string> Packages { get; set; } = new List<string>();
    [JsonPropertyName("UseRequirements")] public bool UseRequirements { get; set; } 
    [JsonPropertyName("IncludeDirectories")] public List<string> IncludeDirectories { get; set; } = new List<string>();
    [JsonPropertyName("IgnorePackages")] public List<string> IgnorePackages { get; set; } = new List<string>();
}
