namespace Pipe.Utils;

public class HelpMessages
{
    public static void VersionMessage()
    {
        Console.WriteLine($"Pipe v{VersionInfo.Version} // Codename {VersionInfo.Codename}");
    }
    public static void BuildHelp()
    {
        VersionMessage();
        Console.WriteLine("\nBuild positional arguments:");
        Console.WriteLine("     help         - Shows help message.");
        Console.WriteLine("     start        - Start build of project.");
        Console.WriteLine("     startverbose - Start build of project with debug logs.");
        Console.WriteLine("     genconfig    - Generating config file.");
        Console.WriteLine("     checkdepends - Checking depends in 'depends_packages'.");
    }
}