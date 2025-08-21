using System.Text;

namespace CS2Sharp.Core;

public static class Extensions
{
    public static void Add(this StringBuilder sb, CodeElementType type, string text = "")
    {
        switch (type)
        {
            case CodeElementType.Using:
                sb.AppendLine($"using {text};");
                break;
            
            case CodeElementType.Namespace:
                sb.AppendLine($"namespace {text};");
                break;
            
            case CodeElementType.LeftBrace:
                sb.AppendLine(text + "{");
                break;
            
            case CodeElementType.RightBrace:
                sb.AppendLine(text + "}");
                break;
            
            case CodeElementType.Comment:
                sb.AppendLine($"// {text}");
                break;
        }
    }
}