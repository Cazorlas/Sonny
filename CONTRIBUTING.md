# Contributing to Sonny

Thank you for your interest in contributing to Sonny! We welcome contributions from the community.

## How to Contribute

### Reporting Bugs

If you find a bug, please create an issue on GitHub with:

- Clear description of the problem
- Steps to reproduce
- Expected behavior vs actual behavior
- Revit version you're using
- Screenshots if applicable
- Error messages or logs if available

### Suggesting Features

We love new ideas! Please create an issue with:

- Clear description of the feature
- Use cases and examples
- Why this would be useful for the Revit community
- Any potential implementation considerations

### Git Flow Workflow

This project follows the **Git Flow** branching model with the following branches:

- **`master`**: Production-ready code. Only stable, tested releases are merged here. This branch should always be deployable.
- **`develop`**: Integration branch for features. All feature branches merge into this branch. This is the main development branch.
- **`feature/*`**: Feature branches for new functionality. Created from and merged back into `develop`.
- **`release/*`**: Release branches for preparing new production releases. Created from `develop` and merged into both `master` and `develop`.
- **`hotfix/*`**: Hotfix branches for urgent production bug fixes. Created from `master` and merged into both `master` and `develop`.

#### Git Flow Diagram

![Git Flow Branching Model](assets/images/gitflow.png)

*Visual representation of the Git Flow branching model showing how commits flow between master, develop, feature, release, and hotfix branches.*

#### Branch Usage Guidelines

**Feature Branches:**
- Created from `develop`
- Used for developing new features
- Merged back into `develop` via Pull Request
- Naming convention: `feature/feature-name` (e.g., `feature/add-new-tool`)

**Release Branches:**
- Created from `develop` when preparing a new release
- Used for final bug fixes and release preparations
- Merged into both `master` (with version tag) and `develop`
- Naming convention: `release/version-number` (e.g., `release/v1.0.0`)
- Only maintainers create release branches

**Hotfix Branches:**
- Created from `master` for urgent production fixes
- Used for critical bug fixes that cannot wait for the next release
- Merged into both `master` (with version tag) and `develop`
- Naming convention: `hotfix/description` (e.g., `hotfix/critical-bug-fix`)
- Only maintainers create hotfix branches

### Pull Requests

**Workflow Overview:**
1. Fork the repository and clone it locally
2. Create a feature branch from `develop` (see Git Flow diagram above)
3. Make your changes and commit them
4. Push your feature branch to your fork
5. Create a Pull Request targeting `develop` branch

**Important Guidelines:**
- **Always create feature branches from `develop`**, not from `master`
- **Pull requests must target `develop` branch** (not `master`)
- Only maintainers merge `develop` into `master` for releases
- Keep your feature branch up to date with `develop` before creating PR

**Commit Message Format:**
Use clear commit messages following this format:
- `Add:` for new features
- `Fix:` for bug fixes
- `Update:` for updates to existing features
- `Refactor:` for code refactoring
- `Docs:` for documentation changes
- `Test:` for test additions or changes

**When Creating a Pull Request:**
- Describe what your PR does
- Reference any related issues (use `Closes #123` or `Fixes #123`)
- Include screenshots if UI changes
- Ensure all tests pass
- Update CHANGELOG.md if applicable

### Release & Hotfix Branches (Maintainers Only)

**Release Branches:**
- Created from `develop` when preparing a new release
- Used for final bug fixes and release preparations
- Merged into both `master` (with version tag) and `develop`
- See Git Flow diagram above for visual workflow

**Hotfix Branches:**
- Created from `master` for urgent production bug fixes
- Used for critical fixes that cannot wait for the next release
- Merged into both `master` (with version tag) and `develop`
- See Git Flow diagram above for visual workflow

## Code Style Guidelines

### C# Coding Standards

- Follow the `.editorconfig` settings
- Use meaningful variable and method names
- Follow C# naming conventions:
  - PascalCase for classes, methods, properties
  - camelCase for local variables
  - _camelCase for private fields
- Add XML documentation comments for public APIs
- Keep methods focused and small
- Use `var` when type is apparent

### Comments

- Write comments in English
- Explain WHY, not WHAT (code should be self-explanatory)
- Update comments when code changes
- Use XML documentation for public APIs

### Testing

- Write unit tests for new features
- Write integration tests for complex workflows
- Ensure all tests pass before submitting PR
- Follow existing test patterns

### Example

```csharp
/// <summary>
///     Converts a value from internal unit to display unit
/// </summary>
/// <param name="value">Value in internal unit (feet)</param>
/// <param name="displayUnit">Target display unit</param>
/// <returns>Converted value in display unit</returns>
public double FromInternalUnit(double value, ForgeTypeId displayUnit)
{
    // Implementation
}
```

## Development Setup

1. Install prerequisites:
   - .NET Framework 4.8
   - .NET 9 SDK
   - Visual Studio or JetBrains Rider
   - Revit (for testing)

2. Clone repository with submodules:
   ```bash
   git clone --recursive https://github.com/your-username/Sonny.git
   cd Sonny
   ```

3. Checkout `develop` branch for development

4. Open solution in your IDE

5. Build the project:
   ```bash
   ./.nuke/build.cmd
   ```

## Testing

- Run unit tests: Use your IDE's test runner or NUnit console
- Run integration tests: Requires Revit to be installed
- Ensure all tests pass before submitting PR

## Questions?

If you have questions about contributing, please:
- Open an issue with the `question` label
- Check existing issues and discussions
- Review the README.md for more information

Thank you for contributing to Sonny! 🎉

