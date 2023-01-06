using Console = System.Console;

namespace Pipe.Utils;

public static class HelpMessages
{
    public static void HeaderMessage()
    {
        Console.WriteLine($"Pipe v{VersionInfo.Version} // RC: {VersionInfo.ReleaseCandidate}");
        Console.WriteLine("usage: pipe <command> <positional>\n");
    }

    public static void HelpProgram()
    {
        HeaderMessage();
        Console.WriteLine("Command:");
        Console.WriteLine("     build - Build project.");
        Console.WriteLine("     info  - Info about Pipe.");
        Console.WriteLine("     help  - Shows this message.");
        Console.WriteLine("     proj  - Manage your project\n");
        Console.WriteLine("Info arguments:");
        Console.WriteLine("     env     - Shows info about environment.");
        Console.WriteLine("     version - Shows pipe version.");
        Console.WriteLine("     req     - Matching requirements with current environment.\n");
        Console.WriteLine("Proj arguments:");
        Console.WriteLine("     init - Initialize a Pipe project.");
        Console.WriteLine("     restore - Install dependencies for project.");
        Console.WriteLine("     depends - List of dependencies.");
        Console.WriteLine("     addpkg  - Add package to recipe."); 
        Console.WriteLine("     rmpkg   - Remove package from recipe.");
        Console.WriteLine("     run     - Run main executable of project."); 
    }
}