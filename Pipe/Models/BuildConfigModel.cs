using System.Text.Json.Serialization;

namespace Pipe.Models;

public class BuildConfigModel
{
    [JsonPropertyName("project_name")]
    public string ProjectName { get; set; } = "pipe_project";
    
    [JsonPropertyName("project_mainexecutable")]
    public string MainExecutableName { get; set; } = "main.py";

    [JsonPropertyName("pipe_buildmodule")]
    public bool ItsModules { get; set; } = false;
    
    [JsonPropertyName("pipe_noconsole")]
    public bool DisableConsole { get; set; } = false;
    
    [JsonPropertyName("pipe_lto")]
    public int LTO { get; set; } = 0;
    
    [JsonPropertyName("pipe_jobs")]
    public int Jobs { get; set; } = 1;
    
    [JsonPropertyName("pipe_useccache")]
    public bool UseCCache { get; set; } = false;
    
    [JsonPropertyName("pipe_usebytecode")]
    public bool UseBytecode { get; set; } = false;
    
    [JsonPropertyName("pipe_cleanafterbuild")]
    public bool CleanAfterBuild { get; set; } = true; 
    
    [JsonPropertyName("options_onefile")]
    public bool OneFile { get; set; } = false;
    
    [JsonPropertyName("options_standalone")]
    public bool StandAlone { get; set; } = false;
    
    [JsonPropertyName("options_followimports")]
    public bool FollowImports { get; set; } = true;
    
    [JsonPropertyName("options_nopyifiles")]
    public bool IgnorePyiFiles { get; set; } = true;
    
    [JsonPropertyName("options_lowmemory")]
    public bool LowMemoryMode { get; set; } = false;

    [JsonPropertyName("depends_packages")]
    public List<string> Packages { get; set; } = new List<string>();
    
    [JsonPropertyName("depends_includedirs")]
    public List<string> IncludeDirectories { get; set; } = new List<string>();
    
    [JsonPropertyName("depends_nofollowto")]
    public List<string> NoFollowTo { get; set; } = new List<string>();
}