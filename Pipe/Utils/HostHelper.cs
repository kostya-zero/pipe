using System.Diagnostics;

namespace Pipe.Utils;

public static class HostHelper
{
    public static int GetThreadsCount()
    {
        Process proc = new Process();
        proc.StartInfo = new ProcessStartInfo
        {
            FileName = "nproc",
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true
        };
        proc.Start();
        string count = proc.StandardOutput.ReadToEnd().TrimEnd('\n');
        proc.WaitForExit();
        return Convert.ToInt32(count);
    }
}