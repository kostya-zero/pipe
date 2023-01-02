using Pipe.Utils;

namespace Pipe.Actions;

public class InfoActions
{
    public void Resolve(string[] args)
    {
        if (args.Length == 1)
        {
            Terminal.Error("Command given, but no positional argument.");
            Terminal.Exit(1);
        }

        switch (args[1])
        {
            case "env":
                Env();
                break;
            case "version":
                Version();
                break;
            case "req":
                Req();
                break;
            default:
                Terminal.Error("Unknown positional argument.");
                Terminal.Exit(1);
                break;
        }
    }

    private void Req()
    {
        Terminal.Info("Checking for requirements...");
        Pip pip = new Pip();
        if (!File.Exists("/usr/bin/python"))
        {
            Terminal.Error("Cannot locate python at '/usr/bin/python'.");
            Terminal.Exit(1);
        }

        if (!pip.Check("nuitka"))
        {
            Terminal.Error("Nuitka not installed! Use 'pip install nuitka' to install it.");
            Terminal.Exit(1); 
        }
        
        Terminal.Done("Everything is OK. You are ready to compile.");
    }

    private void Env()
    {
        Console.WriteLine("Pipe:");
        Console.WriteLine($"     Version:           {VersionInfo.Version}");
        Console.WriteLine($"     Codename:          {VersionInfo.Codename}");
        Console.WriteLine( "     .NET Version:      7.0.101");
        Console.WriteLine($"     Release Candidate: {VersionInfo.ReleaseCandidate}\n");
        Console.WriteLine("System:");
        Console.WriteLine($"     Version:     {Environment.OSVersion.Version}");
        Console.WriteLine($"     User Name:   {Environment.UserName}");
        Console.WriteLine($"     Host Name:   {Environment.MachineName}");
        Console.WriteLine($"     Platform ID: {Environment.OSVersion.Platform.ToString()}\n");
    }

    private void Version()
    {
        Console.WriteLine(VersionInfo.Version);
    }
}