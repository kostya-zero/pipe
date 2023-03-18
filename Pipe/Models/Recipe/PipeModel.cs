using System.Text.Json.Serialization;

namespace Pipe.Models.Recipe;

public class PipeModel
{
    [JsonPropertyName("CheckoutBranch")]
    public string CheckoutBranch { get; set; } = "";
    
    [JsonPropertyName("RunBeforeBuild")]
    public List<string> RunBeforeBuild { get; set; } = new List<string>();
    
    [JsonPropertyName("RequiredTools")]
    public List<string> RequiredTools { get; set; } = new List<string>();
    
    [JsonPropertyName("ClearBuild")]
    public bool ClearBuild { get; set; } = true;
}