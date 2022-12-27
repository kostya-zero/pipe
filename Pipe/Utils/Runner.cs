using System.Diagnostics;
using System.Text;
using Pipe.Models;

namespace Pipe.Utils;

public class Runner
{
    private BuildConfigModel config { get; set; } = new BuildConfigModel();
    private Pip pip { get; set; } = new Pip();

    public void SetConfig(BuildConfigModel configModel)
    {
        config = configModel;
    }

    private void CheckPackages()
    {
        Terminal.Work("Checking for packages...");
        int foundPackages = 0;
        foreach (string package in config.Packages)
        {
            if (pip.CheckPackageInstalled(package))
            {
                foundPackages++;
                Terminal.Info($"Package '{package}' INSTALLED.");
            }
            else
            {
                Terminal.Info($"Package '{package}' NOT INSTALLED.");
            }
        }

        if (foundPackages != config.Packages.Count)
        {
            Terminal.Error($"{(config.Packages.Count - foundPackages).ToString()} packages not found!");
            Terminal.Exit(-4);
        }
        
    }

    private void CheckDirectories()
    {
        Terminal.Work("Checking for directories...");
        int foundDirs = 0;
        foreach (string directory in config.IncludeDirectories)
        {
            if (Directory.Exists(directory))
            {
                foundDirs++;
                Terminal.Info($"Directory '{directory}' EXIST.");
            }
            else
            {
                Terminal.Info($"Directory '{directory}' NOT EXIST.");
            }
        }
        
        if (foundDirs != config.IncludeDirectories.Count)
        {
            Terminal.Error($"{(config.IncludeDirectories.Count - foundDirs).ToString()} directories not found!");
            Terminal.Exit(-4);
        }
    }

    private string GenerateCommand()
    {
        StringBuilder command = new StringBuilder("-m nuitka");
        if (config.Packages.Count != 0)
        {
            foreach (string package in config.Packages)
            {
                command.Append($" --include-package={package}");
            }
        }
        
        if (config.IncludeDirectories.Count != 0)
        {
            foreach (string directory in config.IncludeDirectories)
            {
                command.Append($" --include-plugin-directory={directory}");
            }
        }

        if (config.OneFile)
        {
            command.Append(" --onefile");
        }
        
        if (config.StandAlone)
        {
            command.Append(" --standalone");
        }
        
        if (config.FollowImports)
        {
            command.Append(" --follow-imports");
        }
        
        if (config.IgnorePyiFiles)
        {
            command.Append(" --no-pyi-file");
        }
        
        if (config.LowMemoryMode)
        {
            command.Append(" --low-memory");
        }

        command.Append(" " + config.MainExecutableName);

        return command.ToString();
    }

    public void RunBuild(bool verbose)
    {
        Terminal.Info("Pipe Build System");
        Terminal.Info($"Pipe version: {VersionInfo.Version}");
        Terminal.Info($"Project: {config.ProjectName}");
        if (config.Packages.Count != 0)
        {
            CheckPackages();
        }

        if (config.IncludeDirectories.Count != 0)
        {
            CheckDirectories();
        }
        
        Terminal.Done("All checks complete.");
        Terminal.Work("Making build arguments tree...");
        string command = GenerateCommand();
        Terminal.Work("Preparing to run...");
        Process proc = new Process();
        proc.StartInfo = new ProcessStartInfo
        {
            FileName = "python",
            Arguments = command,
            CreateNoWindow = false
        };
        Terminal.Work("Running nuitka...");
        proc.Start();
        proc.WaitForExit();
        Terminal.Info($"Nuitka finished with exit code -> {proc.ExitCode.ToString()}");
        Terminal.Done("Build finished.");
    }
}