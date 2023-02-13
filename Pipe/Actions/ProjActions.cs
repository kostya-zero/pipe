using System.Diagnostics;
using Pipe.Models;
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
            case "addpkg":
                if (args.Length == 2)
                {
                    Console.WriteLine("Nothing to add to project! Enter a package name after 'addpkg'.");
                    Console.WriteLine("Example: pipe proj addpkg numpy");
                    Terminal.Exit(0);
                }
                
                AddPkg(args[2]); 
                break;
            case "rmpkg":
                if (args.Length == 2)
                {
                    Console.WriteLine("Nothing to remove from project! Enter a package name after 'rmpkg'.");
                    Console.WriteLine("Example: pipe proj rmpkg numpy");
                    Terminal.Exit(1);
                }
                
                RmPkg(args[2]);  
                break;
            
            case "update":
                if (args.Length == 2)
                {
                    Console.WriteLine("Nothing to remove from project! Enter a package name after 'rmpkg'.");
                    Console.WriteLine("Example: pipe proj rmpkg numpy");
                    Terminal.Exit(1);
                } 
                break;
            case "type":
                if (args.Length == 2)
                {
                    Terminal.Info("Set type of you project: app or module.");
                    Terminal.Exit(1);
                }
                
                Type(args[2]);
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
            ProjectName = name,
            MainExecutableName = mainExec
        };
        RecipeManager.MakeRecipe(config);
        if (Git.IsInstalled() && !Git.IsGitRepository())
        {
            if (Git.Init())
            {
                Terminal.Info("Git repository has been initialized.");
            }
        }
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

        if (config.Packages.Count == 0)
        {
            Console.WriteLine("There no dependencies to restore.");
            Terminal.Exit(0);
        }

        Pip pip = new Pip();

        foreach (string package in config.Packages)
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

    private void AddPkg(string pkg)
    {
        Pip pip = new Pip();
        var config = RecipeManager.GetRecipe();
        if (!pip.Check(pkg))
        {
            Console.Write($"Package {pkg} not installed! Do you want to proceed? (Y/n) :");
            var answer = Console.ReadLine()?.Trim();
            switch (answer)
            {
                case "" or "y" or "yes":
                    if (!pip.Install(pkg))
                    {
                        Terminal.Error("Got error while trying to install package.");
                        Terminal.Exit(1);
                    }
                    break;
                case "n" or "no":
                    Console.WriteLine("OK, adding without installation.");
                    break;
                default:
                    if (!pip.Install(pkg))
                    {
                        Terminal.Error("Got error while trying to install package.");
                        Terminal.Exit(1);
                    }
                    break;
            }
        }
        
        config.Packages.Add(pkg);
        RecipeManager.UpdateRecipe(config);
        Console.WriteLine("Package has been added.");
    }

    private void RmPkg(string pkg)
    {
        Pip pip = new Pip();
        var config = RecipeManager.GetRecipe();
        if (!config.Packages.Contains(pkg))
        {
            Console.WriteLine("Package not in this project!");
            Terminal.Exit(0);
        }
        if (pip.Check(pkg))
        {
            config.Packages.Remove(pkg);
            RecipeManager.UpdateRecipe(config);
            Console.WriteLine("Package removed.");
        }
    }

    private void Type(string projectType)
    {
        if (!RecipeManager.CheckForRecipe())
        {
            Terminal.Error("Recipe not found!");
            Terminal.Exit(1);
        }

        var recipe = RecipeManager.GetRecipe();
        projectType = projectType.ToLower();
        switch (projectType)
        {
            case "a" or "app":
                recipe.ProjectType = "app";
                break;
            case "m" or "module":
                recipe.ProjectType = "module";
                break;
            default:
                Terminal.Error("Unknown project type.");
                Terminal.Exit(1);
                break;
        }
        RecipeManager.UpdateRecipe(recipe);
        Terminal.Done("New project type applied.");
    }

    private void Depends()
    {
        if (!RecipeManager.CheckForRecipe())
        {
            Terminal.Error("Recipe not found!");
            Terminal.Exit(1);
        }

        var config = RecipeManager.GetRecipe();
        if (config.Packages.Count == 0)
        {
            Console.WriteLine("No packages added to this project.");
            Terminal.Exit(0);
        }

        foreach (string package in config.Packages)
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
            Arguments = config.MainExecutableName
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