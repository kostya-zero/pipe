using System.Diagnostics;

namespace Pipe.Tools;

public class Pip
{
    public bool Check(string package)
    {
        Process proc = new Process();
        proc.StartInfo = new ProcessStartInfo
        {
            FileName = "pip",
            Arguments = $"show {package}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true
        };
        proc.Start();
        proc.WaitForExit();

        if (proc.ExitCode == 0)
        {
            return true;
        }

        return false;
    }
    
    public bool Install(string package)
    {
        Process proc = new Process();
        proc.StartInfo = new ProcessStartInfo
        {
            FileName = "pip",
            Arguments = $"install {package}"
        };
        proc.Start();
        proc.WaitForExit();

        if (proc.ExitCode == 0)
        {
            return true;
        }

        return false;
    }
    
    public bool Update(string package)
    {
        Process proc = new Process();
        proc.StartInfo = new ProcessStartInfo
        {
            FileName = "pip",
            Arguments = $"install -U {package}"
        };
        proc.Start();
        proc.WaitForExit();
        
        if (proc.ExitCode == 0)
        {
            return true;
        }

        return false;
    }

    public bool InstallFromRequirements()
    {
        Process proc = new Process();
        proc.StartInfo = new ProcessStartInfo
        {
            FileName = "pip",
            Arguments = "install -r requirements.txt",
            CreateNoWindow = true,
            UseShellExecute = false
        };
        proc.Start();
        proc.WaitForExit();

        if (proc.ExitCode == 0)
        {
            return true;
        }

        return false;
    }

}