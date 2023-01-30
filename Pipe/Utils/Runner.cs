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
        int notFoundPackages = 0;
        foreach (string package in Config.Packages)
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
            Terminal.Error("Build failed.");
            Terminal.Exit(4);
        }
        
    }

    private void CheckDirectories()
    {
        Terminal.Work("Checking for directories...");
        int notFoundDirs = 0;
        foreach (string directory in Config.IncludeDirectories)
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
            Terminal.Error("Build failed.");
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

        switch (Config.ProjectType.Trim())
        {
            case "app":
                break;
            case "module":
                command.Append(" --module");
                break;
            default:
                Terminal.Error($"Unknown project type: {Config.ProjectType}.");
                Terminal.Error("Build failed.");
                Terminal.Exit(4);
                break;
    }
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
        bool binaryNotFound = false;
        switch (Config.BackendCompiler.Trim())
        {
            case "gcc":
                Terminal.Work("Searching for GCC binaries...");
                if (File.Exists("/usr/bin/gcc"))
                {
                    Terminal.Done("'/usr/bin/gcc' found.");
                }
                else
                {
                    Terminal.Error("'/usr/bin/gcc' not found!");
                    binaryNotFound = true;
                }
                
                if (File.Exists("/usr/bin/g++"))
                {
                    Terminal.Done("'/usr/bin/g++' found.");
                }
                else
                {
                    Terminal.Error("'/usr/bin/g++' not found!");
                    binaryNotFound = true;
                }

                if (binaryNotFound)
                {
                    Terminal.Error("Cannot continue because some compilers binaries not found.");
                    Terminal.Exit(4);
                }
                break;
            case "clang":
                Terminal.Work("Searching for Clang binaries...");
                if (File.Exists("/usr/bin/clang"))
                {
                    Terminal.Done("'/usr/bin/clang' found.");
                }
                else
                {
                    Terminal.Error("'/usr/bin/clang' not found!");
                    binaryNotFound = true;
                }
                
                if (File.Exists("/usr/bin/clang++"))
                {
                    Terminal.Done("'/usr/bin/clang++' found.");
                }
                else
                {
                    Terminal.Error("'/usr/bin/clang++' not found!");
                    binaryNotFound = true;
                }

                if (binaryNotFound)
                {
                    Terminal.Error("Cannot continue because some compilers binaries not found.");
                    Terminal.Exit(4);
                }
                command.Append(" --clang");
                break;
            case "":
                Terminal.Warn("No compiler specified!");
                Terminal.Error("Build failed.");
                Terminal.Exit(4);
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
        
        Terminal.Info($"Pipe Build System {VersionInfo.Version}");
        if (Config.ProjectDescription.Trim() != "")
        {
            Terminal.Info($"Building {Config.ProjectName} - {Config.ProjectDescription}");
        }
        else { Terminal.Info($"Building {Config.ProjectName}"); }
        
        Terminal.Info($"Configuration: {Config.ProjectType}");
        
        if (Config.Packages.Count != 0) {CheckPackages();}
        if (Config.IncludeDirectories.Count != 0) {CheckDirectories();}
        
        if (!File.Exists(Config.MainExecutableName))
        {
            Terminal.Error($"Main file ({Config.MainExecutableName}) not found.");
            Terminal.Error("Build failed.");
            Terminal.Exit(4);
        }

        if (Config.ProjectVersion.Trim() == "")
        {
            Terminal.Error("Version of project not specified.");
            Terminal.Error("Build failed.");
            Terminal.Exit(4);
        }

        if (Git.IsInstalled() && Git.IsGitRepository())
        {
            string currentBranch = Git.GetBranchName();
            Terminal.Info("Current git branch: " + currentBranch);

            if (Config.CheckoutBranch != currentBranch)
            {
                Terminal.Work($"Checkout '{Config.CheckoutBranch}' branch...");
                Git.Checkout(Config.CheckoutBranch);
            }
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
                    Terminal.Error("Build failed.");
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
            Terminal.Error($"Nuitka exited with bad code ({proc.ExitCode.ToString()}).");
            Terminal.Error("Build failed.");
            Terminal.Exit(4);
        }
        Terminal.Info($"Nuitka finished with exit code {proc.ExitCode.ToString()}.");
        Terminal.Done("Build finished.");
    }
}