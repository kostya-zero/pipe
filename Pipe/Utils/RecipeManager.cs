using System.Text.Json;
using Pipe.Models;

namespace Pipe.Utils;

public class RecipeManager
{
    public static bool CheckForRecipe()
    {
        return File.Exists("recipe.pipe");
    }

    public static void MakeRecipe(RecipeModel config)
    {
        string json = JsonSerializer.Serialize(config, new JsonSerializerOptions{ WriteIndented = true });
        File.Create("recipe.pipe").Close();
        File.WriteAllText("recipe.pipe", json);
    }

    public static void UpdateRecipe(RecipeModel config)
    {
        string json = JsonSerializer.Serialize(config, new JsonSerializerOptions{ WriteIndented = true });
        File.WriteAllText("recipe.pipe", json);
    }

    public static RecipeModel GetRecipe()
    {
        string config = File.ReadAllText("recipe.pipe");
        var configModel = JsonSerializer.Deserialize<RecipeModel>(config);
        return configModel;
    }
}