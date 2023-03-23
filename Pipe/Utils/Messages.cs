using System.Text;
using Pipe.Models;
using Pipe.Tools;
using Console = System.Console;

namespace Pipe.Utils;

public static class Messages
{
    public static void HeaderMessage()
    {
        Console.WriteLine($"{Decorator.Bold}Pipe{Decorator.Reset} v{VersionInfo.Version}");
        Console.WriteLine($"{Decorator.Bold}{Decorator.Underline}Usage:{Decorator.Reset} pipe [command] [argument, ...]\n");
    }

    public static void HelpProgram()
    {
        HeaderMessage();
        Console.WriteLine($"{Decorator.Underline}{Decorator.Bold}Command:{Decorator.Reset}");
        Console.WriteLine($"   {Decorator.Bold}build{Decorator.Reset}  Build project.");
        Console.WriteLine($"   {Decorator.Bold}info{Decorator.Reset}   Info about Pipe.");
        Console.WriteLine($"   {Decorator.Bold}help{Decorator.Reset}   Shows this message.");
        Console.WriteLine($"   {Decorator.Bold}proj{Decorator.Reset}   Manage your project\n");
        Console.WriteLine($"{Decorator.Underline}{Decorator.Bold}Info arguments:{Decorator.Reset}");
        Console.WriteLine($"   {Decorator.Bold}env{Decorator.Reset}      Shows info about environment.");
        Console.WriteLine($"   {Decorator.Bold}version{Decorator.Reset}  Shows pipe version.");
        Console.WriteLine($"   {Decorator.Bold}req{Decorator.Reset}      Matching requirements with current environment.\n");
        Console.WriteLine($"{Decorator.Underline}{Decorator.Bold}Proj arguments:{Decorator.Reset}");
        Console.WriteLine($"   {Decorator.Bold}init{Decorator.Reset}     Initialize a Pipe project.");
        Console.WriteLine($"   {Decorator.Bold}restore{Decorator.Reset}  Install dependencies for project.");
        Console.WriteLine($"   {Decorator.Bold}depends{Decorator.Reset}  List of dependencies.");
        Console.WriteLine($"   {Decorator.Bold}update{Decorator.Reset}   Update each package in project."); 
        Console.WriteLine($"   {Decorator.Bold}run{Decorator.Reset}      Run main executable of project."); 
    }

    public static void Env()
    {
        Console.WriteLine("Pipe:");
        Console.WriteLine($"     Version:           {VersionInfo.Version}");
        Console.WriteLine("System:");
        Console.WriteLine($"     Version:     {Environment.OSVersion.Version}");
        Console.WriteLine($"     User Name:   {Environment.UserName}");
        Console.WriteLine($"     Host Name:   {Environment.MachineName}");
        Console.WriteLine($"     Platform ID: {Environment.OSVersion.Platform.ToString()}\n");
    }

    public static void Require()
    {
        Pip pip = new Pip();
        RequirementsModel requirements = new RequirementsModel();
        Terminal.Info("Trying to find required components...");
        if (File.Exists("/usr/bin/python")) { requirements.FoundPython = true; }

        if (!requirements.FoundPython)
        {
            Terminal.Error("Cannot continue investigation because Python not found.");
            Terminal.Exit(1);
        }

        if (pip.Check("nuitka")) { requirements.FoundNuitka = true; }
        if (File.Exists("/usr/bin/gcc")) { requirements.FoundGcc = true; }
        if (File.Exists("/usr/bin/clang") &&
            File.Exists("/usr/bin/clang++")) { requirements.FoundClang = true; }
        if (File.Exists("/usr/bin/git")) { requirements.FoundGit = true; }

        Console.WriteLine("Investigation completed. Results:");
        Console.WriteLine("Python: " + (requirements.FoundPython ? "Found" : "Not found"));
        Console.WriteLine("Nuitka: " + (requirements.FoundNuitka ? "Found" : "Not found"));
        Console.WriteLine("GCC: " + (requirements.FoundGcc ? "Found" : "Not found") + "\n");
        Console.WriteLine("Optional:");
        Console.WriteLine("Clang: " + (requirements.FoundClang ? "Found" : "Not found"));
        Console.WriteLine("     Clang are needed to build application by using clang as backend compiler");
        Console.WriteLine("Git: " + (requirements.FoundGit ? "Found" : "Not found"));
        Console.WriteLine("     Version control system needed for some projects.");
    }

    public static void Version()
    {
        Console.WriteLine(VersionInfo.Version);
    }
}
