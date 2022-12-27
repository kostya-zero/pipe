namespace Pipe.Models;

public class BuildConfigModel
{
    public string ProjectName { get; set; } = "pipe_project";
    public string MainExecutableName { get; set; } = "main.py";

    public bool OneFile { get; set; } = false;
    public bool StandAlone { get; set; } = false;
    public bool FollowImports { get; set; } = false;
    public List<string> NoFollowTo { get; set; } = new List<string>();
    public bool IgnorePyiFiles { get; set; } = false;
    public bool LowMemoryMode { get; set; } = false;

    public List<string> IncludePackages { get; set; } = new List<string>();
    public List<string> IncludeModules { get; set; } = new List<string>();
    public List<string> IncludeDirectories { get; set; } = new List<string>();
}