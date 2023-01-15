using System.Diagnostics;
using System.Text;
using Pipe.Models;

namespace Pipe.Utils;

public class Runner
{
    private RecipeModel Config { get; set; } = new RecipeModel();
    private Pip Pip { get; } = new Pip();

    public void SetConfig(RecipeModel configModel)
    {
        Config = configModel;
    }

    private void CheckPackages()
    {
        Terminal.Work("Checking for packages...");
        int foundPackages = 0;
        foreach (string package in Config.Packages)
        {
            if (Pip.Check(package))
            {
                foundPackages++;
            }
            else
            {
                Terminal.Info($"Package '{package}' not installed.");
            }
        }

        if (foundPackages != Config.Packages.Count)
        {
            Terminal.Error($"{(Config.Packages.Count - foundPackages).ToString()} packages not found!");
            Terminal.Exit(4);
        }
        
    }

    private void CheckDirectories()
    {
        Terminal.Work("Checking for directories...");
        int foundDirs = 0;
        foreach (string directory in Config.IncludeDirectories)
        {
            if (Directory.Exists(directory))
            {
                foundDirs++;
            }
            else
            {
                Terminal.Info($"Directory '{directory}' not found.");
            }
        }
        
        if (foundDirs != Config.IncludeDirectories.Count)
        {
            Terminal.Error($"{(Config.IncludeDirectories.Count - foundDirs).ToString()} directories not found!");
            Terminal.Exit(4);
        }
    }

    private string GenerateCommand()
    {
        StringBuilder command = new StringBuilder("-m nuitka");
        if (Config.Packages.Count != 0)
        {
            foreach (string package in Config.Packages) {command.Append($" --include-package={package}");}
        }
        
        if (Config.IncludeDirectories.Count != 0)
        {
            foreach (string directory in Config.IncludeDirectories) {command.Append($" --include-plugin-directory={directory}");}
        }

        if (Config.IgnorePkgs.Count != 0)
        {
            foreach (string s in Config.IgnorePkgs) {command.Append($" --nofollow-import-to={s}");}
        }

        command.Append($" --product-version={Config.ProjectVersion.Trim()}");

        if (Config.OneFile) {command.Append(" --onefile");}
        if (Config.StandAlone) {command.Append(" --standalone");}
        if (Config.FollowImports) {command.Append(" --follow-imports");}
        if (Config.IgnorePyiFiles) {command.Append(" --no-pyi-file");}
        
        if (Config.LowMemoryMode) {command.Append(" --low-memory");} 
            Terminal.Warn("Using low memory compilation mode.");

        if (Config.ItsModules) {command.Append(" --module");}
        if (Config.Jobs != 0) {command.Append($" --jobs={Config.Jobs.ToString()}");}

        switch (Config.LTO)
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
                Terminal.Warn($"Option 'pipe_lto' has value that out of range ({Config.LTO.ToString()}). " +
                              "Ignoring.");
                break;
        }

        if (Config.DisableConsole) {command.Append(" --disable-console");}
        switch (Config.BackendCompiler)
        {
            case "gcc":
                break;
            case "clang":
                command.Append(" --clang");
                break;
            case "":
                Terminal.Warn("No compiler specified! Fallback to GCC.");
                break;
            default:
                Terminal.Error($"Unknown compiler: {Config.BackendCompiler}. Using defaults.");
                break;
        }

        command.Append(" " + Config.MainExecutableName);

        return command.ToString();
    }

    public void RunBuild()
    {
        Terminal.Info("Pipe Build System");
        Terminal.Info($"Pipe {VersionInfo.Version}");
        if (Config.ProjectDescription.Trim() != "")
        {
            Terminal.Info($"Building {Config.ProjectName} - {Config.ProjectDescription}");
        }
        else { Terminal.Info($"Building {Config.ProjectName}"); }
        
        string type = Config.ItsModules ? "module" : "app";
        Terminal.Info($"Project type: {type}");
        
        if (Config.Packages.Count != 0) {CheckPackages();}
        if (Config.IncludeDirectories.Count != 0) {CheckDirectories();}
        
        if (!File.Exists(Config.MainExecutableName))
        {
            Terminal.Error($"Main file ({Config.MainExecutableName}) not found. Aborting compilation...");
            Terminal.Exit(4);
        }

        if (Config.ProjectVersion.Trim() == "")
        {
            Terminal.Error("Version of project not specified.");
            Terminal.Exit(4);
        }
        Terminal.Done("All checks complete.");
        if (Config.RunBeforeBuild.Count != 0)
        {
            string shell = Config.CustomShell != "" ? Config.CustomShell : "bash";
            
            Terminal.Work("Running commands before build...");
            foreach (string s in Config.RunBeforeBuild)
            {
                Process commandProc = new Process();
                ProcessStartInfo commandProcInfo = new ProcessStartInfo
                {
                    FileName = shell,
                    Arguments = s,
                    RedirectStandardInput = Config.ShowOnlyErrors,
                    RedirectStandardOutput = Config.ShowOnlyErrors
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
        if (proc.ExitCode != 0)
        {
            Terminal.Error($"Nuitka exited with bad code ({proc.ExitCode.ToString()})");
            Terminal.Exit(4);
        }
        Terminal.Info($"Nuitka finished with exit code -> {proc.ExitCode.ToString()}");
        Terminal.Done("Build finished.");
    }
}