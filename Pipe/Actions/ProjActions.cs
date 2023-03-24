using System.Data;
using System.Diagnostics;
using Pipe.Models;
using Pipe.Tools;
using Pipe.Utils;

namespace Pipe.Actions;

public class ProjActions
{
    public void Resolve(string[] args)
    {
        if (args.Length == 1)
        {
            Terminal.Error("Command given, but no positional argument.");
            Terminal.Exit(-1);
        }

        switch (args[1])
        {
            case "init":
                Init();
                break;
            case "restore":
                Restore();
                break;
            case "depends":
                Depends();
                break;
            case "update":
                if (args.Length == 2)
                {
                    Console.WriteLine("Nothing to remove from project! Enter a package name after 'rmpkg'.");
                    Console.WriteLine("Example: pipe proj rmpkg numpy");
                    Terminal.Exit(1);
                } 
                break;
            case "run":
                Run();
                break;
            default:
                Terminal.Error("Unknown positional argument.");
                Terminal.Exit(-1);
                break;
        }
    }

    private void Init()
    {
        if (RecipeManager.CheckForRecipe())
        {
            Terminal.Error("Recipe already exists!");
            Terminal.Exit(1);
        }
        string name = Terminal.Ask("Enter name of your project.", "pipe_project");
        string mainExec = Terminal.Ask("Enter name of main executable file.", "main.py");
        RecipeModel config = new RecipeModel
        {
            Project =
            {
                Name = name,
                MainExecutable = mainExec
            }
        };
        RecipeManager.MakeRecipe(config);
        Console.WriteLine("Configuration file for your project has been generated!");
        Console.WriteLine("It will be placed with name recipe.pipe.");
    }

    private void Restore()
    {
        if (!RecipeManager.CheckForRecipe())
        {
            Terminal.Error("Recipe not found!");
            Terminal.Exit(1); 
        }

        var config = RecipeManager.GetRecipe();

        if (config.Depends.Packages.Count == 0)
        {
            Console.WriteLine("There no dependencies to restore.");
            Terminal.Exit(0);
        }

        Pip pip = new Pip();

        foreach (string package in config.Depends.Packages)
        {
            if (pip.Check(package))
            {
                Terminal.Info($"Package '{package}' installed. Updating...");
                if (!pip.Update(package))
                {
                    Terminal.Error($"Pip exited with bad exit code while trying to update '{package}'."); 
                }
            }
            else
            {
                if (!pip.Install(package))
                {
                    Terminal.Error($"Pip exited with bad exit code while trying to install '{package}'.");
                }
            }
        }
    }

    private void Depends()
    {
        if (!RecipeManager.CheckForRecipe())
        {
            Terminal.Error("Recipe not found!");
            Terminal.Exit(1);
        }

        var config = RecipeManager.GetRecipe();

        if (config.Depends.UseRequirements)
        {
            if (!File.Exists("requirements.txt"))
            {
                Terminal.Error("requirements.txt are not exists!"); 
                Terminal.Exit(1);
            }
            
            Terminal.Warn("Output is a content of requirements.txt file.");
            string content = File.ReadAllText("requirements.txt");
            Console.WriteLine(content);
        }
        
        if (config.Depends.Packages.Count == 0)
        {
            Console.WriteLine("No packages added to this project.");
            Terminal.Exit(0);
        }

        foreach (string package in config.Depends.Packages)
        {
            Console.WriteLine(package);
        }
    }

    private void Run()
    {
        if (!RecipeManager.CheckForRecipe())
        {
            Terminal.Error("Recipe not found!");
            Terminal.Exit(1);
        }

        var config = RecipeManager.GetRecipe();
        Process proc = new Process();
        ProcessStartInfo procInfo = new ProcessStartInfo
        {
            FileName = "python",
            Arguments = config.Project.MainExecutable
        };
        proc.StartInfo = procInfo;
        proc.Start();
        proc.WaitForExit();

        if (proc.ExitCode == 0)
        {
            Terminal.Done("Process exited with code 0.");
        }
        
        if (proc.ExitCode != 0)
        {
            Terminal.Warn($"Process exited with code {proc.ExitCode.ToString()}");
        } 
    }
}
