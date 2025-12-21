# Sonny

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/)
[![Revit](https://img.shields.io/badge/Revit-2021--2026-orange.svg)](https://www.autodesk.com/products/revit)
[![Build Status](https://img.shields.io/github/actions/workflow/status/PhanCongVuDuc/Sonny/Compile.yml?branch=master)](https://github.com/PhanCongVuDuc/Sonny/actions)

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
* [Learn More](#learn-more)
* [Dependencies](#dependencies)
* [Contributing](#contributing)
* [License](#license)
* [Security](#security)
* [Acknowledgments](#acknowledgments)
<!-- TOC -->

## What You Can Learn

- **Dependency Injection** - Using Microsoft.Extensions.DependencyInjection for IoC
- **Unit Testing** - Writing tests with NUnit and NSubstitute
- **MVVM Pattern** - Implementing MVVM with CommunityToolkit.Mvvm
- **Resource Management / Localization** - Multi-language support with ResourceDictionary and WPFLocalizeExtension
- **Multi-version Support** - Supporting multiple Revit versions (2021-2026)
- **Multiple Units Support** - Handling different measurement units (feet, meters, inches, etc.)
- **Async Programming** - Using async/await with Revit API
- **Build Automation** - Using Nuke for build automation
- **MSBuild Customization** - Custom MSBuild targets and tasks

## Videos

### AutoColumnDimension Demo

<a href="http://www.youtube.com/watch?feature=player_embedded&v=ue2QgaLX7fE" target="_blank"><img src="http://img.youtube.com/vi/ue2QgaLX7fE/0.jpg" alt="AutoColumnDimension Demo" width="560" height="315" border="10" /></a>

### ColumnFromCad Demo

<a href="http://www.youtube.com/watch?feature=player_embedded&v=D6tkBJeo9uo" target="_blank"><img src="http://img.youtube.com/vi/D6tkBJeo9uo/0.jpg" alt="ColumnFromCad Demo" width="560" height="315" border="10" /></a>

### Unit Testing by Console

<a href="http://www.youtube.com/watch?feature=player_embedded&v=L4UvIr6Km7g" target="_blank"><img src="http://img.youtube.com/vi/L4UvIr6Km7g/0.jpg" alt="Unit Testing by Console" width="560" height="315" border="10" /></a>

### Unit Testing by Visual Studio

<a href="http://www.youtube.com/watch?feature=player_embedded&v=exr4cEYcHmk" target="_blank"><img src="http://img.youtube.com/vi/exr4cEYcHmk/0.jpg" alt="Unit Testing by Visual Studio" width="560" height="315" border="10" /></a>

### Units & Languages Support

<a href="http://www.youtube.com/watch?feature=player_embedded&v=DQbC8JEZ5BM" target="_blank"><img src="http://img.youtube.com/vi/DQbC8JEZ5BM/0.jpg" alt="Units & Languages Support" width="560" height="315" border="10" /></a>

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
- **Sonny.EasyRibbon** - Attribute-based framework for creating Revit Ribbon UI
- **Sonny.RevitExtensions** - Revit API extension methods and utilities library

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
git add source/Revit.Async source/Sonny.EasyRibbon source/Sonny.RevitExtensions
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

#### Sonny.EasyRibbon
- **Submodule location**: `source/Sonny.EasyRibbon`
- **Submodule repository**: [Sonny.EasyRibbon](https://github.com/PhanCongVuDuc/Sonny.EasyRibbon)
- The submodule tracks a specific commit from the Sonny.EasyRibbon repository
- Changes to Sonny.EasyRibbon should be committed in its own repository, not in Sonny

#### Sonny.RevitExtensions
- **Submodule location**: `source/Sonny.RevitExtensions`
- **Submodule repository**: [Sonny.RevitExtensions](https://github.com/PhanCongVuDuc/Sonny.RevitExtensions)
- The submodule tracks a specific commit from the Sonny.RevitExtensions repository
- Changes to Sonny.RevitExtensions should be committed in its own repository, not in Sonny

> [!NOTE]
> If you see empty submodule folders after cloning, you need to initialize submodules using `git submodule update --init --recursive`

## Solution Structure

| Folder  | Description                                                                |
|---------|----------------------------------------------------------------------------|
| build   | Nuke build system. Used to automate project builds                         |
| install | Add-in installer, called implicitly by the Nuke build                      |
| source  | Project source code folder. Contains all solution projects                 |
| output  | Folder of generated files by the build system, such as bundles, installers |

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
- **[Sonny.EasyRibbon](https://github.com/PhanCongVuDuc/Sonny.EasyRibbon)** - Attribute-based framework for creating Revit Ribbon UI
- **[Sonny.RevitExtensions](https://github.com/PhanCongVuDuc/Sonny.RevitExtensions)** - Revit API extension methods and utilities library

## Contributing

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for details on:

- How to report bugs
- How to suggest features
- How to submit pull requests
- Code style guidelines

Please read our [Code of Conduct](CODE_OF_CONDUCT.md) before contributing.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Security

If you discover a security vulnerability, please see our [Security Policy](SECURITY.md) for information on how to report it.

## Acknowledgments

We would like to express our gratitude to the creators of the following open-source libraries:

- **[RevitTemplates](https://github.com/Nice3point/RevitTemplates)** by [Nice3point](https://github.com/Nice3point) - Project templates and build system for Revit plugins
- **[Revit.Async](https://github.com/PhanCongVuDuc/Revit.Async)** by [KennanChan](https://github.com/KennanChan) - Async utilities for Revit API
- **[RevitTest](https://github.com/ricaun-io/RevitTest)** by [ricaun-io](https://github.com/ricaun-io) - Testing framework for Revit applications
