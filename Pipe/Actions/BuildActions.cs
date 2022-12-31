using System.Text.Json;
using Pipe.Models;
using Pipe.Utils;

namespace Pipe.Actions;

public class BuildActions
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
            case "start":
                Start();
                break;
            case "genconfig":
                GenConfig();
                break;
            case "checkdepends":
                CheckDepends();
                break;
            case "help":
                HelpMessages.BuildHelp();
                break;
            default:
                Terminal.Error("Unknown positional argument.");
                Terminal.Exit(-1);
                break;
        }
    }

    private void GenConfig()
    {
        if (Configs.CheckForConfig())
        {
            Terminal.Error("Config already exists!");
            Terminal.Exit(1);
        }
        Terminal.Work("Starting genconfig script.");
        string name = Terminal.Ask("Name of project.", "pipe_project");
        string mainExec = Terminal.Ask("Name of main executable file.", "main.py");
        Terminal.Work("Generating config model...");
        BuildConfigModel config = new BuildConfigModel
        {
            ProjectName = name,
            MainExecutableName = mainExec
            
        };
        Terminal.Work("Writing to file.");
        Configs.MakeConfig(config);
        Terminal.Done("Config generated.");
    }

    private void Start()
    {
        if (!Configs.CheckForConfig())
        {
            Terminal.Error("Config not found!");
            Terminal.Exit(1); 
        }
        Runner runner = new Runner();
        runner.SetConfig(Configs.GetConfig());
        runner.RunBuild();
    }
    
    private void CheckDepends()
    {
        if (!Configs.CheckForConfig())
        {
            Terminal.Error("Config not found!");
            Terminal.Exit(1); 
        }

        var config = Configs.GetConfig();
        if (config.Packages.Count == 0)
        {
            Console.WriteLine("Nothing to check!");
            Terminal.Exit(0);
        }

        Pip pip = new Pip();
        
        if (config.Packages.Count != 0)
        {
            Terminal.Info($"Total packages: {config.Packages.Count.ToString()}");
            foreach (string package in config.Packages)
            {
                if (pip.CheckPackageInstalled(package))
                {
                    Terminal.Good($"Package '{package}' found.");
                }
                else
                {
                    Terminal.Warn($"Package '{package}' not found."); 
                }
            }
        } 
    }
}