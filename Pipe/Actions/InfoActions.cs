using Pipe.Utils;

namespace Pipe.Actions;

public class InfoActions
{
    public void Resolve(string[] args)
    {
        if (args.Length == 1)
        {
            Terminal.Error("Command given, but no positional argument.");
            Terminal.Exit(-1);
        }

        switch (args[0])
        {
            case "env":
                break;
            case "version":
                break;
            case "req":
                break;
            default:
                Terminal.Error("Unknown positional argument.");
                Terminal.Exit(-1);
                break;
        }
    }
}