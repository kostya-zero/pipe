using Pipe.Models;
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
        Pip pip = new Pip();
        RequirementsModel requirements = new RequirementsModel();
        Terminal.Info("Trying to find required components...");
        if (File.Exists("/usr/bin/python")) requirements.FoundPython = true;

        if (!requirements.FoundPython)
        {
            Terminal.Error("Cannot continue investigation because Python not found.");
            Terminal.Exit(1);
        }

        if (pip.Check("nuitka")) requirements.FoundNuitka = true;
        if (File.Exists("/usr/bin/gcc")) requirements.FoundGcc = true;
        if (File.Exists("/usr/bin/clang") &&
            File.Exists("/usr/bin/clang++")) requirements.FoundClang = true;
        
        Console.WriteLine("Investigation completed. Results:");
        Console.WriteLine("Python: " + (requirements.FoundPython ? "Found" : "Not found"));
        Console.WriteLine("Nuitka: " + (requirements.FoundNuitka ? "Found" : "Not found"));
        Console.WriteLine("GCC: " + (requirements.FoundGcc ? "Found" : "Not found"));
        Console.WriteLine("Optional:");
        Console.WriteLine("Clang: " + (requirements.FoundClang ? "Found" : "Not found"));
        Console.WriteLine("     Clang are needed to build application by using clang as backend compiler");
    }

    private void Env()
    {
        Console.WriteLine("Pipe:");
        Console.WriteLine($"     Version:           {VersionInfo.Version}");
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