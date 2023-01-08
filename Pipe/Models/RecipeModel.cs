using System.Text.Json.Serialization;

namespace Pipe.Models;

public class RecipeModel
{
    [JsonPropertyName("project_name")]
    public string ProjectName { get; set; } = "pipe_project";
    
    [JsonPropertyName("project_mainexecutable")]
    public string MainExecutableName { get; set; } = "main.py";
    
    [JsonPropertyName("project_version")]
    public string ProjectVersion { get; set; } = "1.0.0";

    [JsonPropertyName("project_descritpion")] 
    public string ProjectDescription { get; set; } = "An App built with Pipe!";

    [JsonPropertyName("project_author")]
    public string ProjectAuthor { get; set; } = "Me";

    [JsonPropertyName("pipe_buildmodule")]
    public bool ItsModules { get; set; } = false;
    
    [JsonPropertyName("pipe_noconsole")]
    public bool DisableConsole { get; set; } = false;
    
    [JsonPropertyName("nuitka_lto")]
    public int LTO { get; set; }
    
    [JsonPropertyName("nuitka_jobs")]
    public int Jobs { get; set; } = 1;
    
    [JsonPropertyName("nuitka_useccache")]
    public bool UseCCache { get; set; }
    
    [JsonPropertyName("pipe_runbeforebuild")]
    public List<string> RunBeforeBuild { get; set; } = new List<string>();
    
    [JsonPropertyName("pipe_customshell")]
    public string CustomShell { get; set; }
    
    [JsonPropertyName("nuitka_useclang")]
    public bool UseClang { get; set; }
    
    [JsonPropertyName("options_onefile")]
    public bool OneFile { get; set; } = false;
    
    [JsonPropertyName("options_standalone")]
    public bool StandAlone { get; set; }
    
    [JsonPropertyName("options_followimports")]
    public bool FollowImports { get; set; } = true;
    
    [JsonPropertyName("options_nopyifiles")]
    public bool IgnorePyiFiles { get; set; } = true;
    
    [JsonPropertyName("options_lowmemory")]
    public bool LowMemoryMode { get; set; } = false;
    
    [JsonPropertyName("options_showonlyerror")]
    public bool ShowOnlyErrors { get; set; }

    [JsonPropertyName("depends_packages")]
    public List<string> Packages { get; set; } = new List<string>();
    
    [JsonPropertyName("depends_includedirs")]
    public List<string> IncludeDirectories { get; set; } = new List<string>();
    
    [JsonPropertyName("depends_ignorepkgs")]
    public List<string> IgnorePkgs { get; set; } = new List<string>(new [] { "email", "http"});
}