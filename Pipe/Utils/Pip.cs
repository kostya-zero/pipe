using System.Diagnostics;

namespace Pipe.Utils;

public class Pip
{
    public bool Check(string package)
    {
        Process proc = new Process();
        bool result = false;
        ProcessStartInfo procInfo = new ProcessStartInfo
        {
            FileName = "/usr/bin/pip",
            Arguments = $"show {package}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true
        };
        proc.StartInfo = procInfo;
        proc.Start();
        proc.WaitForExit();

        if (proc.ExitCode == 0)
        {
            result = true;
        }

        return result;
    }
    
    public bool Install(string package, bool noOutput = false)
    {
        Process proc = new Process();
        bool result = false;
        ProcessStartInfo procInfo = new ProcessStartInfo
        {
            FileName = "/usr/bin/pip",
            Arguments = $"install {package}"
        };

        if (noOutput)
        {
            procInfo.RedirectStandardOutput = true;
            procInfo.RedirectStandardError = true;
        }
        proc.StartInfo = procInfo;
        proc.Start();
        proc.WaitForExit();

        if (proc.ExitCode == 0)
        {
            result = true;
        }

        return result;
    }
    
    public bool Update(string package, bool noOutput = false)
    {
        Process proc = new Process();
        bool result = false;
        ProcessStartInfo procInfo = new ProcessStartInfo
        {
            FileName = "/usr/bin/pip",
            Arguments = $"install -U {package}"
        };

        if (noOutput)
        {
            procInfo.RedirectStandardOutput = true;
            procInfo.RedirectStandardError = true;
        }
        proc.StartInfo = procInfo;
        proc.Start();
        proc.WaitForExit();

        if (proc.ExitCode == 0)
        {
            result = true;
        }

        return result;
    }
}