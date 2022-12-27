namespace Pipe.Utils;

public class Terminal
{
    public static void Error(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Error ");
        Console.ResetColor();
        Console.WriteLine($": {msg}");
    }

    public static void Info(string msg)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("Info ");
        Console.ResetColor();
        Console.WriteLine($": {msg}"); 
    }

    public static void Exit(int exitCode)
    {
        Environment.Exit(exitCode);
    }

    public static void Work(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Work ");
        Console.ResetColor();
        Console.WriteLine($": {msg}");  
    }
    
    public static void Done(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Done ");
        Console.ResetColor();
        Console.WriteLine($": {msg}");  
    }
    
    public static void Good(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Good ");
        Console.ResetColor();
        Console.WriteLine($": {msg}");  
    }
    
    public static void Warn(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Warn ");
        Console.ResetColor();
        Console.WriteLine($": {msg}");  
    }

    public static string Ask(string msg, string defaultValue)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(msg);
        Console.ResetColor();
        Console.Write($" ({defaultValue}) ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(": ");
        Console.ResetColor();
        string value = Console.ReadLine().Trim();
        if (value == "")
        {
            value = defaultValue;
        }
        return value;
    }
}