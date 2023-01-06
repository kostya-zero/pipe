using Pipe.Actions;
using Pipe.Utils;

namespace Pipe;

class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Nothing to do.");
            Terminal.Exit(1);
        }

        switch (args[0])
        {
            case "build":
                if (!RecipeManager.CheckForRecipe())
                {
                    Terminal.Error("Config not found!");
                    Terminal.Exit(1); 
                }
                Runner runner = new Runner();
                runner.SetConfig(RecipeManager.GetRecipe());
                runner.RunBuild();
                break;
            case "proj":
                ProjActions projActions = new ProjActions();
                projActions.Resolve(args);
                break;
            case "info":
                InfoActions infoActions = new InfoActions();
                infoActions.Resolve(args);
                break;
            case "help":
                HelpMessages.HelpProgram();
                break;
            default:
                Terminal.Error("Unknown command.");
                Terminal.Exit(1);
                break;
        }
    }
}