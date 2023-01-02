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
                break;
            case "run":
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
                bool updateResult = pip.Update(package);
                if (!updateResult)
                {
                    Terminal.Error($"Pip exited with bad exit code while trying to update '{package}'."); 
                }
            }
            else
            {
                bool installResult = pip.Install(package);

                if (!installResult)
                {
                    Terminal.Error($"Pip exited with bad exit code while trying to install '{package}'.");
                }
            }
        }
    }
}