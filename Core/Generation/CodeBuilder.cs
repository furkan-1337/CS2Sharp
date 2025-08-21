using System.Text;
using CS2Sharp.Core;
using CS2Sharp.Core.Models;

namespace CS2Sharp.src.Generation;

public class CodeBuilder
{
    public static void Build(GameModule module, string outputDir = "SDK")
    {
        Console.WriteLine($"\n[SDK] Module: {module.Name}");
        string moduleDir = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(module.Name));
        
        if (!string.IsNullOrEmpty(moduleDir) && !Directory.Exists(moduleDir))
            Directory.CreateDirectory(moduleDir);
        
        foreach (var gameClass in module.Classes)
        {
            var code = Build(gameClass);
            string fileName = gameClass.Name + ".cs";
            string fullPath = string.IsNullOrEmpty(moduleDir) 
                ? fileName 
                : Path.Combine(moduleDir, fileName);

            File.WriteAllText(fullPath, code);
        }
        Console.WriteLine($"[SDK] {module.Name} generated. ({module.Classes.Count} classes.)");
    }
    
    public static string Build(GameClass gameClass)
    {
        StringBuilder sb = new();
        // Usings
        sb.Add(CodeElementType.Using,"System.Numerics");
        sb.Add(CodeElementType.Using,"System.Drawing");
        sb.AppendLine();
        
        // Namespace
        sb.Add(CodeElementType.Namespace,"CS2Sharp.SDK");
        sb.AppendLine();
        
        // Class
        var buildInfo = DumpProvider.GetDumpInfo();
        sb.Add(CodeElementType.Comment,$"INFO: Build Number: {buildInfo.BuildNumber} - Timestamp: {buildInfo.Timestamp}");
        sb.AppendLine($"public class {gameClass.Name}{(gameClass.HasParent ? $" : {gameClass.Parent}" : "")}");
        sb.Add(CodeElementType.LeftBrace);
        sb.AppendLine("    public nint Address { get; private set; }");
        sb.AppendLine();
        sb.AppendLine($"    public {gameClass.Name}(nint address){(gameClass.HasParent ? $" : base(address)" : "")}");
        sb.AppendLine("    {");
        sb.AppendLine("        Address = address;");
        sb.AppendLine("    }");
        if(gameClass.Fields.Count != 0)
            sb.AppendLine();
        
        for (int i = 0; i < gameClass.Fields.Count; i++)
        {
            var field = gameClass.Fields[i];
            sb.AppendLine(Build(field));
        
            // Son field değilse boş satır ekle
            if (i < gameClass.Fields.Count - 1)
                sb.AppendLine();
        }
        sb.Add(CodeElementType.RightBrace);
        
        Console.WriteLine($"[Class] {gameClass.Name} generated. ({gameClass.Fields.Count} netvars)");
        return sb.ToString();
    }

    public static string Build(NetvarField field)
    {
        StringBuilder sb = new();
        string typename = TypeConverter.AsCSharpType(field.Type);
        if (string.IsNullOrEmpty(field.Description) && typename != field.Type)
            field.Description = $"Original type: {TypeConverter.GetOriginalTypeName(field.Type)}";

        string read = $"Game.Memory.Read<{typename}>";
        string write = $"Game.Memory.Write<{typename}>";

        if (typename == "string")
            read = "Game.Memory.ReadAsString";
        
        sb.AppendLine($"    public {typename} {field.Name}{(field.HasDescription ? $" // {field.Description}" : "")}");
        sb.Add(CodeElementType.LeftBrace, "    ");
        sb.AppendLine($"        get => {read}(Address + 0x{field.Value:X});");
        
        if(typename != "string")
            sb.AppendLine($"        set => {write}(Address + 0x{field.Value:X}, value);");
        sb.Add(CodeElementType.RightBrace, "    ");
        return sb.ToString().TrimEnd();
    }
}