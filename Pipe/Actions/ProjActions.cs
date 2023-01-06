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
                    Terminal.Exit(0);
                }
                
                RmPkg(args[2]);  
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
        if (Configs.CheckForConfig())
        {
            Terminal.Error("Config already exists!");
            Terminal.Exit(1);
        }
        string name = Terminal.Ask("Enter name of your project.", "pipe_project");
        string mainExec = Terminal.Ask("Enter name of main executable file.", "main.py");
        Terminal.Work("Generating config model...");
        BuildConfigModel config = new BuildConfigModel
        {
            ProjectName = name,
            MainExecutableName = mainExec
        };
        Configs.MakeConfig(config);
        Console.WriteLine("Configuration file for your project has been generated!");
        Console.WriteLine("It will be placed with name project.pipe.");
    }

    private void Restore()
    {
        if (!Configs.CheckForConfig())
        {
            Terminal.Error("Config not found!");
            Terminal.Exit(1); 
        }

        var config = Configs.GetConfig();

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
        var config = Configs.GetConfig();
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
                    Console.WriteLine("OK, adding without installation.");
                    break;
            }
        }
        
        config.Packages.Add(pkg);
        Configs.UpdateConfig(config);
        Console.WriteLine("Package has been added.");
    }

    private void RmPkg(string pkg)
    {
        Pip pip = new Pip();
        var config = Configs.GetConfig();
        if (!config.Packages.Contains(pkg))
        {
            Console.WriteLine("Package not in this project!");
            Terminal.Exit(0);
        }
        if (pip.Check(pkg))
        {
            config.Packages.Remove(pkg);
            Console.WriteLine("Package removed.");
        }
    }

    private void Run()
    {
        if (!Configs.CheckForConfig())
        {
            Terminal.Error("Config not found!");
            Terminal.Exit(1);
        }

        var config = Configs.GetConfig();
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