namespace CS2Sharp.Core.Models;

public struct NetvarField
{
    public string Name;
    public string Type;
    public int Value;
    public string? Description;
    
    public bool HasDescription => !string.IsNullOrEmpty(Description);
    public NetvarField(string name, string type, int value)
    {
        Name = name;
        Type = type;
        Value = value;
    }

    public NetvarField(string name, string type, int value, string description) : this(name, type, value)
    {
        Description = description;
    }
}