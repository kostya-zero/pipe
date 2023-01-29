using System.Diagnostics;

namespace Pipe.Utils;

public class Git
{
    public static bool IsInstalled()
    {
        return File.Exists("/usr/bin/git");
    }
    
    public static bool IsGitRepository()
    {
        Process proc = new Process();
        proc.StartInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = "status",
            RedirectStandardError = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };
        proc.Start();
        proc.WaitForExit();
        if (proc.ExitCode == 128)
        {
            return false;
        }

        return true;
    }

    public static string GetBranchName()
    {
        Process proc = new Process();
        proc.StartInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = "branch --show-current",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };
        proc.Start();
        string name = proc.StandardOutput.ReadToEnd().TrimEnd('\n');
        proc.WaitForExit();
        return name;
    }

    public static bool Checkout(string branch)
    {
        Process proc = new Process();
        proc.StartInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = $"checkout {branch}",
            RedirectStandardError = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };
        proc.Start();
        proc.WaitForExit();
        if (proc.ExitCode != 0)
        {
            return false;
        }

        return true; 
    }
}