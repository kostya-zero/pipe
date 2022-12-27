using Pipe.Actions;
using Pipe.Utils;

namespace Pipe;

class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Nothing to do.");
            Terminal.Exit(-1);
        }

        switch (args[0])
        {
            case "build":
                BuildActions buildActions = new BuildActions();
                buildActions.Resolve(args);
                break;
            case "config":
                break;
            case "info":
                break;
            default:
                Terminal.Error("Unknown command.");
                Terminal.Exit(-1);
                break;
        }
    }
}