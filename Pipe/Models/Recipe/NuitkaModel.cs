using System.Text.Json.Serialization;

namespace Pipe.Models.Recipe;

public class NuitkaModel
{
    [JsonPropertyName("BackendCompiler")]
    public string BackendCompiler { get; set; } = "gcc";

    [JsonPropertyName("LTO")]
    public int LTO { get; set; }

    [JsonPropertyName("Jobs")]
    public int Jobs { get; set; }

    [JsonPropertyName("UseLibpython")]
    public bool UseLibpython { get; set; }

    [JsonPropertyName("ClearCache")]
    public bool ClearCache { get; set; }

    [JsonPropertyName("ShowScons")]
    public bool ShowScons { get; set; }
}
