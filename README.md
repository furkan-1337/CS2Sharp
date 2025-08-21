# CS2Sharp
Counter-Strike 2, SDK Generator based on [/a2x/cs2-dumper](https://github.com/a2x/cs2-dumper)

**⚠️ For educational purposes only. This project is intended to demonstrate SDK generation concepts and should not be used for cheating or modifying the game.**

## Features
- Parses game module schemas to generate C# SDK code.  
- Converts game types to appropriate C# types.  
- Supports generation of classes with inheritance.  
- Fetches module dump files from a remote source.  
- Includes build number and timestamp in generated code comments.  

```bash
├── Core/
│   ├── CodeElementType.cs       # Defines the types of code elements (e.g., Using, Namespace).
│   ├── Extensions.cs            # Provides extension methods for StringBuilder to add code elements.
│   ├── Generation/              # Contains classes responsible for generating C# code.
│   │   ├── CodeBuilder.cs       # Builds the C# code for classes and fields.
│   │   ├── DumpProvider.cs      # Provides access to module dump files.
│   │   ├── SchemaParser.cs      # Parses the game module schema.
│   │   ├── TypeConverter.cs     # Converts game types to C# types.
│   ├── Models/                  # Defines the data models for game modules, classes, and netvars.
│   │   ├── GameClass.cs         # Represents a game class.
│   │   ├── GameModule.cs        # Represents a game module.
│   │   ├── NetvarField.cs       # Represents a netvar field.
├── CS2Sharp.csproj              # C# project file.
├── LICENSE                      # License file.
├── Program.cs                   # Main program file.
└── README.md                    # This file.
```
