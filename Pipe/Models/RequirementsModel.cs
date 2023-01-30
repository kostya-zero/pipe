namespace Pipe.Models;

public class RequirementsModel
{
   public bool FoundPython { get; set; }
   public bool FoundNuitka { get; set; }
   public bool FoundGcc    { get; set; }
   public bool FoundClang  { get; set; }
   public bool FoundGit  { get; set; }
}