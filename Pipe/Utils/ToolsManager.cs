using Pipe.Tools;

namespace Pipe.Utils;

public class ToolsManager
{
    public static bool FindPython()
    {
        if (File.Exists("/usr/bin/python") ||
            File.Exists("/usr/bin/python3"))
        {
            return true;
        }

        return false;
    }

    public static bool FindClang()
    {
        if (File.Exists("/usr/bin/clang") &&
            File.Exists("/usr/bin/clang++"))
        {
            return true;
        }
        
        return false;
    }

    public static bool FindNuitka()
    {
        Pip pip = new Pip();
        if (pip.Check("nuitka"))
        {
            return true;
        }

        return false;
    }
        
    public static bool FindGcc()
    {
        if (File.Exists("/usr/bin/gcc") &&
            File.Exists("/usr/bin/g++"))
        {
            return true;
        }

        return false;
    }

}
