using CS2Sharp.src.Generation;

namespace CS2Sharp;

class Program
{
    static void Main(string[] args)
    {
        var client = SchemaParser.ParseModule("client.dll");
        CodeBuilder.Build(client);
        Console.WriteLine("Done!");
        Console.ReadKey();
    }
}