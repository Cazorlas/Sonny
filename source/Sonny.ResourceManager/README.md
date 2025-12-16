# ResourceManager

A lightweight, reusable ResourceDictionary manager for WPF applications with multi-language support.

## Features

- ✅ Centralized ResourceDictionary management
- ✅ Multi-language support
- ✅ Integration with WPFLocalizeExtension
- ✅ Priority-based loading
- ✅ Fallback language support
- ✅ Event-driven culture changes
- ✅ Thread-safe singleton pattern

## Installation

```bash
dotnet add package ResourceManager
```

Or via NuGet Package Manager:

```
Install-Package ResourceManager
```

## Quick Start

### Basic Usage

```csharp
using ResourceManager;

// Get instance
var manager = ResourceDictionaryManager.Instance;

// Register a resource
manager.RegisterResource(
    resourceId: "MyRibbon",
    resourceName: "Ribbon",
    assemblyName: "MyResources",
    pluginName: "Main",
    defaultLanguageCode: "en"
);

// Load all resources
manager.LoadAllResources("en");

// Change language
manager.ChangeLanguage("vi");
```

### Advanced Usage

#### Custom Path Pattern

```csharp
manager.RegisterResourceWithCustomPath(
    resourceId: "CustomStrings",
    fullResourcePath: "Resources/Strings.{LanguageCode}.xaml",
    assemblyName: "MyResources",
    defaultLanguageCode: "en"
);
```

#### Priority-based Loading

```csharp
// Load this first (priority 0)
manager.RegisterResource("BaseResources", "Base", "MyResources", priority: 0);

// Load this after (priority 1)
manager.RegisterResource("ThemeResources", "Theme", "MyResources", priority: 1);
```

#### Event Handling

```csharp
manager.CultureChanged += (sender, e) =>
{
    Console.WriteLine($"Culture changed to: {e.Culture.Name}");
};
```

## Resource File Structure

Your resource files should follow this structure:

```
MyResources/
└── Languages/
    └── Main/
        ├── Ribbon.en.xaml
        ├── Ribbon.vi.xaml
        └── Ribbon.ja.xaml
```

Or with custom path:

```
MyResources/
└── Resources/
    ├── Strings.en.xaml
    ├── Strings.vi.xaml
    └── Strings.ja.xaml
```

## API Reference

### ResourceDictionaryManager

#### Properties

- `Instance` - Gets the singleton instance
- `CurrentCulture` - Gets or sets the current culture
- `CurrentLanguageCode` - Gets the current language code (e.g., "en", "vi")

#### Methods

- `RegisterResource(...)` - Register a ResourceDictionary configuration
- `RegisterResourceWithCustomPath(...)` - Register with custom path pattern
- `LoadResource(resourceId, languageCode)` - Load a specific resource
- `LoadAllResources(languageCode)` - Load all registered resources
- `UnloadResource(resourceId)` - Unload a specific resource
- `ChangeLanguage(languageCode)` - Change language and reload all resources
- `IsRegistered(resourceId)` - Check if resource is registered
- `GetRegisteredResourceIds()` - Get all registered resource IDs
- `ClearRegisteredResources()` - Clear all registered resources

#### Events

- `CultureChanged` - Raised when culture is changed

## Requirements

- .NET Framework 4.8 or .NET 8.0+
- WPF
- WPFLocalizeExtension 3.9.0+

## License

MIT License

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

