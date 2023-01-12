using System.Text.Json.Serialization;

namespace Pipe.Models;

public class RecipeModel
{
    [JsonPropertyName("Project_Name")]
    public string ProjectName { get; set; } = "pipe_project";
    
    [JsonPropertyName("Project_MainExecutable")]
    public string MainExecutableName { get; set; } = "main.py";
    
    [JsonPropertyName("Project_Version")]
    public string ProjectVersion { get; set; } = "1.0.0";

    [JsonPropertyName("Project_Descritpion")] 
    public string ProjectDescription { get; set; } = "An App built with Pipe!";

    [JsonPropertyName("Project_Author")]
    public string ProjectAuthor { get; set; } = "Me";

    [JsonPropertyName("Pipe_BuildModule")]
    public bool ItsModules { get; set; } = false;
    
    [JsonPropertyName("Pipe_NoConsole")]
    public bool DisableConsole { get; set; } = false;
    
    [JsonPropertyName("Pipe_BackendCompiler")]
    public string BackendCompiler { get; set; } = "gcc";
    
    [JsonPropertyName("Nuitka_LTO")]
    public int LTO { get; set; }
    
    [JsonPropertyName("Nuitka_Jobs")]
    public int Jobs { get; set; } = 1;
    
    [JsonPropertyName("Nuitka_UseCcache")]
    public bool UseCCache { get; set; }
    
    [JsonPropertyName("Pipe_RunBeforeBuild")]
    public List<string> RunBeforeBuild { get; set; } = new List<string>();

    [JsonPropertyName("Pipe_CustomShell")] 
    public string CustomShell { get; set; } = "";
    
    [JsonPropertyName("Options_OneFile")]
    public bool OneFile { get; set; } = false;
    
    [JsonPropertyName("Options_Standalone")]
    public bool StandAlone { get; set; }
    
    [JsonPropertyName("Options_FollowImports")]
    public bool FollowImports { get; set; } = true;
    
    [JsonPropertyName("Options_NoPyiFiles")]
    public bool IgnorePyiFiles { get; set; } = true;
    
    [JsonPropertyName("Options_LowMemory")]
    public bool LowMemoryMode { get; set; } = false;
    
    [JsonPropertyName("Options_ShowOnlyError")]
    public bool ShowOnlyErrors { get; set; }

    [JsonPropertyName("Depends_Packages")]
    public List<string> Packages { get; set; } = new List<string>();
    
    [JsonPropertyName("Depends_IncludeDirs")]
    public List<string> IncludeDirectories { get; set; } = new List<string>();
    
    [JsonPropertyName("Depends_IgnorePkgs")]
    public List<string> IgnorePkgs { get; set; } = new List<string>(new [] { "email", "http"});
}