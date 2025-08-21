namespace CS2Sharp.Core.Models;

public struct GameClass
{
    public GameModule Module;
    public string Name;
    public List<NetvarField> Fields;
    public string? Parent;
    
    public bool HasParent => !string.IsNullOrEmpty(Parent);
    public GameClass(GameModule module, string name)
    {
        Module = module;
        Name = name;
        Fields = new List<NetvarField>();
    }
}