using System.Diagnostics;
using System.Text;
using Pipe.Models;
using Pipe.Tools;

namespace Pipe.Utils;

public class Runner
{
    private RecipeModel Config { get; set; } = new RecipeModel();
    private Pip Pip { get; } = new Pip();

    public void SetConfig(RecipeModel configModel)
    {
        Config = configModel;
    }

    private void FailBuild()
    {
        Terminal.Error("Build failed.");
        Terminal.Exit(1);
    }

    private void CheckPackages()
    {
        Terminal.Build("Checking for packages...");
        int notFoundPackages = 0;
        foreach (string package in Config.Depends.Packages)
        {
            if (Pip.Check(package))
            {
                Terminal.Good($"Package '{package}' installed."); 
            }
            else
            {
                Terminal.Error($"Package '{package}' not installed.");
                notFoundPackages++;
            }
        }

        if (notFoundPackages != 0)
        {
            Terminal.Error($"{notFoundPackages.ToString()} packages not found!");
            FailBuild();
        }
        
    }

    private void CheckDirectories()
    {
        Terminal.Build("Checking for directories...");
        int notFoundDirs = 0;
        foreach (string directory in Config.Depends.IncludeDirectories)
        {
            if (Directory.Exists(directory))
            {
                Terminal.Good($"Directory '{directory}' found.");
            }
            else
            {
                Terminal.Error($"Directory '{directory}' not found.");
                notFoundDirs++;
            }
        }
        
        if (notFoundDirs != 0)
        {
            Terminal.Error($"{notFoundDirs.ToString()} directories not found!");
            FailBuild();
        }
    }

    private void CheckConflicts()
    {
        foreach (string package in Config.Depends.Packages)
        {
            if (Config.Depends.IgnorePackages.Contains(package))
            {
                Terminal.Error($"Pipe confused, because {package} package are used and ignored at the same time.");
                FailBuild();
            }
        }
    }

    private string GenerateCommand()
    {
        StringBuilder command = new StringBuilder("-m nuitka");
        if (Config.Depends.Packages.Count != 0)
        {
            foreach (string package in Config.Depends.Packages) {command.Append($" --include-package={package}");}
        }
        
        if (Config.Depends.IncludeDirectories.Count != 0)
        {
            foreach (string directory in Config.Depends.IncludeDirectories) {command.Append($" --include-plugin-directory={directory}");}
        }

        if (Config.Depends.IgnorePackages.Count != 0)
        {
            foreach (string s in Config.Depends.IgnorePackages) {command.Append($" --nofollow-import-to={s}");}
        }

        command.Append($" --product-version={Config.Project.Version.Trim()} --file-version={Config.Project.Version.Trim()}");

        if (Config.Options.OneFile) {command.Append(" --onefile");}
        if (Config.Options.StandAlone) {command.Append(" --standalone");}
        if (Config.Options.FollowImports) {command.Append(" --follow-imports");}
        if (Config.Options.IgnorePyiFiles) {command.Append(" --no-pyi-file");}
        if (Config.Nuitka.Jobs != 0) {command.Append($" --jobs={Config.Nuitka.Jobs.ToString()}");}

        if (Config.Nuitka.Jobs == 0)
        {
            Terminal.Build("Detecting threads ('Nutika_Jobs': 0)...");
            int count = HostHelper.GetThreadsCount();
            Terminal.Done($"Found {count.ToString()} threads.");
            command.Append($" --jobs={count.ToString()}");
        }

        switch (Config.Nuitka.LTO)
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
                Terminal.Warn($"Option 'Nuitka_LTO' has value '{Config.Nuitka.LTO.ToString()}' that out of range. " +
                              "Ignoring.");
                break;
        }

        bool binaryNotFound = false;
        switch (Config.Nuitka.BackendCompiler.Trim())
        {
            case "gcc":
                Terminal.Build("Searching for GCC binaries...");
                if (!ToolsManager.FindGcc())
                {
                    binaryNotFound = true;
                }
                break;
            case "clang":
                Terminal.Warn("Using clang as backend!");
                Terminal.Build("Searching for Clang binaries...");
                if (!ToolsManager.FindClang())
                {
                    binaryNotFound = true;
                }
                command.Append(" --clang");
                break;
            case "":
                Terminal.Warn("No compiler specified!");
                FailBuild();
                break;
            default:
                Terminal.Error($"Unknown compiler: {Config.Nuitka.BackendCompiler}. Using defaults.");
                break;
        }
        
        if (binaryNotFound)
        {
            Terminal.Error("Cannot continue because some compilers binaries not found.");
            FailBuild();
        }
        
        switch(Config.Project.Type) {
            case "app":
                command.Append(" " + Config.Project.MainExecutable);
                break;
            case "module":
                command.Append(" --module " + Config.Project.MainExecutable);
                break;
            default:
                Terminal.Error("Incorrect project type!");
                FailBuild();
                break;
        }

        return command.ToString();
    }

    public void RunBuild()
    {
        Terminal.Info($"Pipe Build System {VersionInfo.Version}");
        Terminal.Info(Config.Project.Description.Trim() != ""
            ? $"Building {Config.Project.Name} - {Config.Project.Description}"
            : $"Building {Config.Project.Name}");
        Terminal.Info("Making version " + Config.Project.Version);
        Terminal.Info($"Configuration: {Config.Project.Type}");
        
        Terminal.Build("Searching for Python installation...");
        if (!File.Exists("/usr/bin/python") || !File.Exists("/usr/bin/python3"))
        {
            Terminal.Error("Python not found!");
            FailBuild();
        }
        Terminal.Good("Python found.");

        if (Config.Depends.UseRequirements)
        {
            if (!File.Exists("requirements.txt"))
            {
                Terminal.Error("requirements.txt are not exists!");
                FailBuild();
            }
            Terminal.Warn("Using requirements.txt file. Pipe are not tracking this packages.");
            if (!Pip.InstallFromRequirements())
            {
                Terminal.Error("Pip got bad exit code.");
                FailBuild();
            }
        }
        else
        {
            if (Config.Depends.Packages.Count != 0)
            {
                CheckPackages();
                CheckConflicts();
            } 
        }
        
        if (Config.Depends.IncludeDirectories.Count != 0) {CheckDirectories();}
        
        if (!File.Exists(Config.Project.MainExecutable))
        {
            Terminal.Error($"Main file ({Config.Project.MainExecutable}) not found.");
            FailBuild();
        }

        if (Config.Project.Version.Trim() == "")
        {
            Terminal.Error("Version of project not specified.");
            FailBuild();
        }
        
        Terminal.Done("All checks complete.");
        Terminal.Build("Making build arguments tree...");
        string command = GenerateCommand();
        Terminal.Build("Preparing to run...");
        Process proc = new Process();
        proc.StartInfo = new ProcessStartInfo
        {
            FileName = "python",
            Arguments = command,
            CreateNoWindow = false,
            RedirectStandardInput = Config.Options.ShowOnlyErrors,
            RedirectStandardOutput = Config.Options.ShowOnlyErrors 
        };
        Terminal.Build("Running nuitka...");
        proc.Start();
        proc.WaitForExit();
        if (proc.ExitCode != 0)
        {
            Terminal.Error($"Nuitka exited with bad code ({proc.ExitCode.ToString()}).");
            FailBuild();
        }
        string finalName = Path.GetFileNameWithoutExtension(Config.Project.MainExecutable);
        Terminal.Info($"Nuitka finished with exit code {proc.ExitCode.ToString()}.");
        Terminal.Done($"Build finished. Executable will be saved as '{finalName + ".bin"}'.");
    }
}
