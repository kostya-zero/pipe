namespace Pipe.Utils;

public class HelpMessages
{
    public static void HeaderMessage()
    {
        Console.WriteLine($"Pipe v{VersionInfo.Version} // Codename: {VersionInfo.Codename} // RC: {VersionInfo.ReleaseCandidate}");
        Console.WriteLine("usage: pipe <command> <positional>\n");
    }

    public static void HelpProgram()
    {
        HeaderMessage();
        Console.WriteLine("Command:");
        Console.WriteLine("     build - Build project.");
        Console.WriteLine("     info  - Info about Pipe.");
        Console.WriteLine("     help  - Shows this message.");
    }
    public static void BuildHelp()
    {
        HeaderMessage();
        Console.WriteLine("Build positional arguments:");
        Console.WriteLine("     help         - Shows help message.");
        Console.WriteLine("     start        - Start build of project.");
        Console.WriteLine("     genconfig    - Generating config file.");
        Console.WriteLine("     checkdepends - Checking depends in 'depends_packages'.");
    }

    public static void InfoHelp()
    {
        HeaderMessage();
        Console.WriteLine("Info arguments:");
        Console.WriteLine("     env     - Shows info about environment.");
        Console.WriteLine("     version - Shows pipe version.");
        Console.WriteLine("     req     - Matching requirements with current environment.");
    }
}