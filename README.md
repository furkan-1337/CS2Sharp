# CS2Sharp
Counter-Strike 2, SDK Generator based on [/a2x/cs2-dumper](https://github.com/a2x/cs2-dumper)

## Features
- Parses game module schemas to generate C# SDK code.  
- Converts game types to appropriate C# types.  
- Supports generation of classes with inheritance.  
- Fetches module dump files from a remote source.  
- Includes build number and timestamp in generated code comments.  

```bash
CS2Sharp/
├── Core/
│   ├── CodeElementType.cs
│   │   - Enum: C# kod oluşturma sırasında kullanılan element tiplerini tanımlar
│   │   - Örn: Using, Namespace
│   │
│   ├── Extensions.cs
│   │   - StringBuilder için extension metodları içerir
│   │   - Örn: AddUsing, SetNamespace
│   │
│   ├── Generation/
│   │   ├── CodeBuilder.cs
│   │   │   - GameClass modellerinden C# kodu üretir
│   │   │   - Class, property ve field tanımlamalarını oluşturur
│   │   │
│   │   ├── DumpProvider.cs
│   │   │   - Oyun modülüne ait dump dosyalarını sağlar
│   │   │   - Uzaktan ya da lokaldeki dump dosyalarını okur
│   │   │
│   │   ├── SchemaParser.cs
│   │   │   - Dump dosyalarını parse ederek GameModule ve GameClass modellerine dönüştürür
│   │   │
│   │   ├── TypeConverter.cs
│   │       - Oyun tiplerini uygun C# tiplerine çevirir
│   │
│   ├── Models/
│   │   ├── GameClass.cs
│   │   │   - Bir oyun class’ını temsil eder
│   │   │   - Netvar field’ları ve inheritance bilgilerini içerir
│   │   │
│   │   ├── GameModule.cs
│   │   │   - Bir oyun modülünü temsil eder (örn: client.dll)
│   │   │
│   │   ├── NetvarField.cs
│   │       - Bir netvar field’ını temsil eder
│   │       - Name, Type, Offset bilgilerini içerir
│
├── CS2Sharp.csproj
│
```
