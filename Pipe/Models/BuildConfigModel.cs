using System.Text.Json.Serialization;

namespace Pipe.Models;

public class BuildConfigModel
{
    [JsonPropertyName("project_name")]
    public string ProjectName { get; set; } = "pipe_project";
    
    [JsonPropertyName("project_mainexecutable")]
    public string MainExecutableName { get; set; } = "main.py";

    [JsonPropertyName("options_onefile")]
    public bool OneFile { get; set; } = false;
    
    [JsonPropertyName("options_standalone")]
    public bool StandAlone { get; set; } = false;
    
    [JsonPropertyName("options_followimports")]
    public bool FollowImports { get; set; } = false;
    
    [JsonPropertyName("options_nopyifiles")]
    public bool IgnorePyiFiles { get; set; } = false;
    
    [JsonPropertyName("options_lowmemory")]
    public bool LowMemoryMode { get; set; } = false;

    [JsonPropertyName("depends_packages")]
    public List<string> Packages { get; set; } = new List<string>();
    
    [JsonPropertyName("depends_includedirs")]
    public List<string> IncludeDirectories { get; set; } = new List<string>();
    
    [JsonPropertyName("depends_nofollowto")]
    public List<string> NoFollowTo { get; set; } = new List<string>();
}