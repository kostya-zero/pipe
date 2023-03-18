using System.Text.Json.Serialization;

namespace Pipe.Models.Recipe;

public class OptionsModel 
{
    [JsonPropertyName("OneFile")] public bool OneFile { get; set; }
    [JsonPropertyName("StandAlone")] public bool StandAlone { get; set; }
    [JsonPropertyName("FollowImports")] public bool FollowImports { get; set; } = true;
    [JsonPropertyName("IgnorePyiFiles")] public bool IgnorePyiFiles { get; set; } = true;
    [JsonPropertyName("ShowOnlyErrors")] public bool ShowOnlyErrors { get; set; }

}
