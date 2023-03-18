using System.Text.Json.Serialization;
using Pipe.Models.Recipe;

namespace Pipe.Models;

public class RecipeModel
{

    [JsonPropertyName("Project")] public ProjectModel Project { get; set; } = new ProjectModel();

    [JsonPropertyName("Nuitka")] public NuitkaModel Nuitka { get; set; } = new NuitkaModel();

    [JsonPropertyName("Pipe")] public PipeModel Pipe { get; set; } = new PipeModel();
    
    [JsonPropertyName("Options")] public OptionsModel Options { get; set; } = new OptionsModel();

    [JsonPropertyName("Depends")] public DependsModel Depends { get; set; } = new DependsModel();
}
