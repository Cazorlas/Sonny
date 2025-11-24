# Sonny

Autodesk Revit plugin project organized into multiple solution files that target versions 2021 - 2026.

## Introduction

Sonny is an **open source** project built with the following goals:

- 📚 **Learning**: An educational project for collaborative research and development of Revit API programming skills
- 🤝 **Community**: Together we develop, share knowledge and experiences
- 🆓 **Free**: Create free tools for everyone to use

This project focuses on developing useful tools for the Revit community, with open source code so that everyone can learn, contribute, and use freely.

## Table of content

<!-- TOC -->
* [What You Can Learn](#what-you-can-learn)
* [Videos](#videos)
* [Prerequisites](#prerequisites)
* [Cloning the Repository](#cloning-the-repository)
* [Solution Structure](#solution-structure)
* [Managing Supported Revit Versions](#managing-supported-revit-versions)
* [Learn More](#learn-more)
* [Dependencies](#dependencies)
* [Acknowledgments](#acknowledgments)
<!-- TOC -->

## What You Can Learn

- **Dependency Injection** - Using Microsoft.Extensions.DependencyInjection for IoC
- **Unit Testing** - Writing tests with NUnit and NSubstitute
- **MVVM Pattern** - Implementing MVVM with CommunityToolkit.Mvvm
- **Multi-version Support** - Supporting multiple Revit versions (2021-2026)
- **Multiple Units Support** - Handling different measurement units (feet, meters, inches, etc.)
- **Async Programming** - Using async/await with Revit API
- **WPF UI Development** - Building user interfaces with WPF
- **Build Automation** - Using Nuke for build automation
- **MSBuild Customization** - Custom MSBuild targets and tasks
- **Logging** - Structured logging with Serilog
- **Revit API** - Working with Revit API and extension methods

## Videos

### AutoColumnDimension Demo

<video src="assets/videos/AutoColumnDimension demo.mp4" controls width="800"></video>

### Unit Testing by Console

<video src="assets/videos/UnitTest by console.mp4" controls width="800"></video>

### Unit Testing by Visual Studio

<video src="assets/videos/UnitTest by visual studio.mp4" controls width="800"></video>

## Prerequisites

Before you can build this project, you need to install .NET and IDE.
If you haven't already installed these, you can do so by visiting the following:

- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- [.NET 9](https://dotnet.microsoft.com/en-us/download/dotnet)
- [JetBrains Rider](https://www.jetbrains.com/rider/) or [Visual Studio](https://visualstudio.microsoft.com/)

After installation, clone this repository to your local machine and navigate to the project directory.

## Cloning the Repository

This project uses **Git Submodule** to manage dependencies. The following libraries are included as submodules from their own repositories:

- **Revit.Async** - Async utilities for Revit API
- **EasyRibbon** - Attribute-based framework for creating Revit Ribbon UI

### Clone with Submodules

When cloning this repository, you need to initialize and update submodules to get all dependencies:

```bash
# Clone with submodules (recommended)
git clone --recursive <repository-url>

# Or if you already cloned without --recursive
git submodule update --init --recursive
```

### Updating Submodules

To update submodules to their latest commits:

```bash
# Update submodules to latest commits
git submodule update --remote

# Commit and push the submodule reference update to GitHub
git add source/Revit.Async source/EasyRibbon
git commit -m "Update submodules to latest version"
git push origin master
```

### Working with Submodules

This project includes the following submodules:

#### Revit.Async
- **Submodule location**: `source/Revit.Async`
- **Submodule repository**: [Revit.Async](https://github.com/PhanCongVuDuc/Revit.Async)
- The submodule tracks a specific commit from the Revit.Async repository
- Changes to Revit.Async should be committed in its own repository, not in Sonny

#### EasyRibbon
- **Submodule location**: `source/EasyRibbon`
- **Submodule repository**: [EasyRibbon](https://github.com/PhanCongVuDuc/EasyRibbon)
- The submodule tracks a specific commit from the EasyRibbon repository
- Changes to EasyRibbon should be committed in its own repository, not in Sonny

> [!NOTE]
> If you see an empty `source/Revit.Async` folder after cloning, you need to initialize submodules using `git submodule update --init --recursive`

## Solution Structure

| Folder  | Description                                                                |
|---------|----------------------------------------------------------------------------|
| build   | Nuke build system. Used to automate project builds                         |
| install | Add-in installer, called implicitly by the Nuke build                      |
| source  | Project source code folder. Contains all solution projects                 |
| output  | Folder of generated files by the build system, such as bundles, installers |

## Managing Supported Revit Versions

To extend or reduce the range of supported Revit API versions, you need to update the solution and project configurations.

### Solution configurations

Solution configurations determine which projects are built and how they are configured.

To support multiple Revit versions:
- Open the `.sln` file.
- Add or remove configurations for each Revit version.

Example:

```text
GlobalSection(SolutionConfigurationPlatforms) = preSolution
    Debug R24|Any CPU = Debug R24|Any CPU
    Debug R25|Any CPU = Debug R25|Any CPU
    Debug R26|Any CPU = Debug R26|Any CPU
    Release R24|Any CPU = Release R24|Any CPU
    Release R25|Any CPU = Release R25|Any CPU
    Release R26|Any CPU = Release R26|Any CPU
EndGlobalSection
```

For example `Debug R26` is the Debug configuration for Revit 2026 version.

> [!TIP]
> If you are just ending maintenance for some version, removing the Solution configurations without modifying the Project configurations is enough.

### Project configurations

Project configurations define build conditions for specific versions.

To add or remove support:
- Open `.csproj` file
- Add or remove configurations for Debug and Release builds.

Example:

```xml
<PropertyGroup>
    <Configurations>Debug R24;Debug R25;Debug R26</Configurations>
    <Configurations>$(Configurations);Release R24;Release R25;Release R26</Configurations>
</PropertyGroup>
```

> [!IMPORTANT]
> Edit the `.csproj` file only manually, IDEs often break configurations.

Then simply map the solution configuration to the project configuration:

![image](https://github.com/user-attachments/assets/9f357ded-d38c-4f0a-a21f-152de75f4abc)

Solution and project configuration names may differ, this example uses the same naming style to avoid confusion.

Then specify the framework and Revit version for each configuration, update the `.csproj` file with the following:

```xml
<PropertyGroup Condition="$(Configuration.Contains('R26'))">
    <RevitVersion>2026</RevitVersion>
    <TargetFramework>net8.0-windows</TargetFramework>
</PropertyGroup>
```

## Learn More

For detailed documentation, see [RevitTemplates Wiki](https://github.com/Nice3point/RevitTemplates/wiki):

- [Building](https://github.com/Nice3point/RevitTemplates/wiki) - Build with IDE or NUKE
- [Publishing Releases](https://github.com/Nice3point/RevitTemplates/wiki) - Create releases and tags
- [Compiling on GitHub](https://github.com/Nice3point/RevitTemplates/wiki) - CI/CD pipelines
- [Conditional Compilation](https://github.com/Nice3point/RevitTemplates/wiki) - Multi-version support
- [API References](https://github.com/Nice3point/RevitTemplates/wiki) - NuGet packages for CI/CD

## Dependencies

This project uses the following libraries and tools:

- **[RevitTemplates](https://github.com/Nice3point/RevitTemplates)** - Project templates and build system for Revit plugins
- **[Revit.Async](https://github.com/PhanCongVuDuc/Revit.Async)** - Async utilities for Revit API
- **[RevitTest](https://github.com/ricaun-io/RevitTest)** - Testing framework for Revit applications
- **[EasyRibbon](https://github.com/PhanCongVuDuc/EasyRibbon)** - Attribute-based framework for creating Revit Ribbon UI
- **[SonnyRevitExtensions](https://github.com/PhanCongVuDuc/SonnyRevitExtensions)** - Revit API extension methods and utilities library

## Acknowledgments

We would like to express our gratitude to the creators of the following open-source libraries:

- **[RevitTemplates](https://github.com/Nice3point/RevitTemplates)** by [Nice3point](https://github.com/Nice3point) - Project templates and build system for Revit plugins
- **[Revit.Async](https://github.com/PhanCongVuDuc/Revit.Async)** by [KennanChan](https://github.com/KennanChan) - Async utilities for Revit API
- **[RevitTest](https://github.com/ricaun-io/RevitTest)** by [ricaun-io](https://github.com/ricaun-io) - Testing framework for Revit applications
