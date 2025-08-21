using System.Collections.ObjectModel;
using System.Text.Json;
using CS2Sharp.Core.Models;

namespace CS2Sharp.src.Generation;

public class SchemaParser
{
    public static GameModule ParseModule(string module)
    {
        GameModule gameModule = new GameModule(module);
        string context = DumpProvider.GetModuleDump(module);
        JsonDocument doc = JsonDocument.Parse(context);
        
        var moduleProperty = doc.RootElement.GetProperty(module);
        var classes = moduleProperty.GetProperty("classes");
        
        foreach (var classProperty in classes.EnumerateObject())
        {
            var gameClass = ParseClass(gameModule, classProperty);
            gameModule.Classes.Add(gameClass);
        }
        
        return gameModule;
    }

    public static IReadOnlyList<string> MissingParents { get; } = new List<string>() { "CPlayerPawnComponent", "CPlayerControllerComponent", "CSkeletonAnimationController" };
    public static GameClass ParseClass(GameModule module, JsonProperty classElement)
    {
        GameClass gameClass = new GameClass(module, classElement.Name);
        
        var fields = classElement.Value.GetProperty("fields");
        var metadatas = classElement.Value.GetProperty("metadata");
        
        if (classElement.Value.TryGetProperty("parent", out JsonElement parentElement))
        {
            string parentName = parentElement.GetString();
            if (!string.IsNullOrEmpty(parentName))
                gameClass.Parent = parentName;
        }

        if (gameClass.Parent != null)
        {
            foreach (var missingParent in MissingParents)
                if(gameClass.Parent.Equals(missingParent))
                    gameClass.Parent = string.Empty;
        }
        
        foreach (var metadata in metadatas.EnumerateArray())
        {
            bool hasTypeName = metadata.TryGetProperty("type_name", out JsonElement typename);
            if(!hasTypeName)
                continue;
            
            string netvarName = metadata.GetProperty("name").GetString();
            string typeName = typename.GetString();
            int value = fields.GetProperty(netvarName).GetInt32();
            gameClass.Fields.Add(new NetvarField(netvarName, typeName, value));
        }

        return gameClass;
    }
}