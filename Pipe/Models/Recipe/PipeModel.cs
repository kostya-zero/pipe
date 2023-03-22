using System.Text.Json.Serialization;

namespace Pipe.Models.Recipe;

public class PipeModel
{
    [JsonPropertyName("ClearBuild")]
    public bool ClearBuild { get; set; } = true;
}
