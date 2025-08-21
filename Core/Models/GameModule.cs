namespace CS2Sharp.Core.Models;

public struct GameModule
{
    public string Name;
    public List<GameClass> Classes;
    
    public GameModule(string name)
    {
        Name = name;
        Classes = new List<GameClass>();
    }
}