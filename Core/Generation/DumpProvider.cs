using System.Net;
using System.Text.Json;

namespace CS2Sharp.src.Generation;

public class DumpProvider
{
    public static readonly string BaseUrl = "https://raw.githubusercontent.com/a2x/cs2-dumper/refs/heads/main/output/";
    public static Dictionary<string, string> Dumps { get; set; } = new();
    private static WebClient WebClient { get; set; } = new();
    
    public static string GetModuleDump(string moduleName)
    {
        string moduleContent = string.Empty;
        string url = $"{BaseUrl}{moduleName.Replace(".", "_")}.json";
        if (!Dumps.TryGetValue(url, out moduleContent))
        {
            moduleContent = WebClient.DownloadString(url);
            Dumps.Add(url, moduleContent);
        }
        return moduleContent;
    }
    
    public static (int BuildNumber, string Timestamp) GetDumpInfo()
    {
        string content = GetModuleDump("info");
        JsonDocument doc = JsonDocument.Parse(content);
        
        int buildNumber = doc.RootElement.GetProperty("build_number").GetInt32();
        string timestamp = doc.RootElement.GetProperty("timestamp").GetString().Split('.')[0];
        return (buildNumber, timestamp);
    }
}