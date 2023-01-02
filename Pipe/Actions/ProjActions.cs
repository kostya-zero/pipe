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
}