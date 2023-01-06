using System.Diagnostics;
using System.Text;
using Pipe.Models;

namespace Pipe.Utils;

public class Runner
{
    private BuildConfigModel config { get; set; } = new BuildConfigModel();
    private Pip pip { get; } = new Pip();

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
            if (pip.Check(package))
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
            Terminal.Exit(4);
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
            Terminal.Exit(4);
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

        if (config.NoFollowTo.Count != 0)
        {
            foreach (string s in config.NoFollowTo)
            {
                command.Append($" --nofollow-import-to={s}");
            }
        }

        command.Append($" --product-version={config.ProjectVersion.Trim()}");

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
            Terminal.Warn("Using low memory compilation mode.");
        }

        if (config.ItsModules)
        {
            command.Append(" --module");
        }

        if (config.Jobs != 0)
        {
            command.Append($" --jobs={config.Jobs.ToString()}");
        }

        switch (config.LTO)
        {
            case 0:
                break;
            case 1:
                command.Append(" --lto=yes");
                Terminal.Warn("LTO enabled.");
                break;
            case 2:
                command.Append(" --lto=auto");
                Terminal.Warn("LTO set to auto.");
                break;
            default:
                Terminal.Warn($"Option 'pipe_lto' has value that out of range ({config.LTO.ToString()}). " +
                              "Ignoring.");
                break;
        }

            if (config.DisableConsole)
        {
            command.Append(" --disable-console");
        }

        if (!config.UseBytecode)
        {
            command.Append(" --disable-bytecode-cache"); 
        }
        
        if (!config.UseCCache)
        {
            command.Append(" --disable-ccache"); 
        }

        command.Append(" " + config.MainExecutableName);

        return command.ToString();
    }

    public void RunBuild()
    {
        Terminal.Info("Pipe Build System");
        Terminal.Info($"Pipe version: {VersionInfo.Version}");
        Terminal.Info($"Project: {config.ProjectName}");
        string type = config.ItsModules ? "module" : "app";
        Terminal.Info($"Project type: {type}");
        if (config.Packages.Count != 0)
        {
            CheckPackages();
        }

        if (config.IncludeDirectories.Count != 0)
        {
            CheckDirectories();
        }

        if (!File.Exists(config.MainExecutableName))
        {
            Terminal.Error($"Main file ({config.MainExecutableName}) not found. Aborting compilation...");
            Terminal.Exit(4);
        }

        if (config.ProjectVersion.Trim() == "")
        {
            Terminal.Error("Version of project not specified.");
            Terminal.Exit(4);
        }
        Terminal.Done("All checks complete.");
        if (config.RunBeforeBuild.Count != 0)
        {
            Terminal.Work("Running commands before build...");
            foreach (string s in config.RunBeforeBuild)
            {
                Process commandProc = new Process();
                ProcessStartInfo commandProcInfo = new ProcessStartInfo
                {
                    FileName = "/usr/bin/bash",
                    Arguments = s
                };
                commandProc.StartInfo = commandProcInfo;
                Terminal.Work(s);
                commandProc.Start();
                commandProc.WaitForExit();
                if (commandProc.ExitCode != 0)
                {
                    Terminal.Error($"Command '{s}' exited with bad code ({commandProc.ExitCode.ToString()}).");
                    Terminal.Exit(4);
                }
            }
        }
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