using System.Text;
using Console = System.Console;

namespace Pipe.Utils;

public static class HelpMessages
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
        Console.WriteLine($"   {Decorator.Bold}type{Decorator.Reset}     Set type to your project. Can be 'app' or 'module'."); 
        Console.WriteLine($"   {Decorator.Bold}addpkg{Decorator.Reset}   Add package to recipe.");
        Console.WriteLine($"   {Decorator.Bold}update{Decorator.Reset}   Update each package in project."); 
        Console.WriteLine($"   {Decorator.Bold}rmpkg{Decorator.Reset}    Remove package from recipe.");
        Console.WriteLine($"   {Decorator.Bold}run{Decorator.Reset}      Run main executable of project."); 
    }
}
