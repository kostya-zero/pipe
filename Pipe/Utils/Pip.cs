using System.Diagnostics;

namespace Pipe.Utils;

public class Pip
{
    public bool CheckPackageInstalled(string package)
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

        if (proc.ExitCode == 1)
        {
            result = false;
        }

        if (proc.ExitCode != 1)
        {
            result = true;
        }

        return result;
    }

}