# 1.2.0

## Features

- **License Management** - License management system with Keygen API integration
- **User Authentication** - OIDC authentication via Auth0 for secure login
- **Login Command** - Login command to authenticate and manage licenses
- **Auto Login** - Automatic login with token persistence
- **Hybrid Licensing** - Support for both online and offline license validation
- **Trial License** - Automatic trial license creation for new users
- **License File Management** - Offline license file checkout with TTL support

## Architecture

- **Clean Architecture** - Refactored project to follow Clean Architecture principles with clear layer separation
- **Domain Layer** - Core business entities and interfaces, framework-agnostic
- **UseCases Layer** - Application business rules and use case implementations
- **Infrastructure Layer** - Framework implementations (Revit API, external services)
- **Presentation Layer** - UI components, ViewModels, and user interface

# 1.1.0

## Features

- **ColumnFromCad** - Create columns from CAD link files (rectangular and circular columns)
- **CAD Link Integration** - Extract column geometry from linked CAD files
- **Multiple Column Types** - Support for both rectangular and circular column families
- **Settings Persistence** - Save and restore ColumnFromCad settings

# 1.0.0

## Features

- **AutoColumnDimension** - Automatically create dimensions for columns in Revit
- **Multi-language Support** - Multi-language support with ResourceDictionary and WPFLocalizeExtension
- **Settings Dialog** - Settings dialog for the plugin
- **Multi-version Support** - Support for Revit 2021 - 2026
- **Multiple Units Support** - Support for multiple measurement units (feet, meters, inches, etc.)

## Architecture

- **Dependency Injection** - Using Microsoft.Extensions.DependencyInjection
- **MVVM Pattern** - Implementing MVVM with CommunityToolkit.Mvvm
- **Unit Testing** - Testing framework with NUnit and NSubstitute

## Build & Development

- **Build Automation** - Build automation with Nuke
- **MSBuild Customization** - Custom MSBuild targets and tasks
