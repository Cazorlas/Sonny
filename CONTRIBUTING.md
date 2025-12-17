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

### Pull Requests

1. **Fork the repository**
   ```bash
   git clone https://github.com/your-username/Sonny.git
   cd Sonny
   ```

2. **Initialize submodules**
   ```bash
   git submodule update --init --recursive
   ```

3. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

4. **Make your changes**
   - Write clean, readable code
   - Follow the existing code style (see `.editorconfig`)
   - Add XML documentation comments for public APIs
   - Test your changes thoroughly
   - Update documentation if needed

5. **Commit your changes**
   ```bash
   git commit -m "Add: brief description of your changes"
   ```

   Use clear commit messages following this format:
   - `Add:` for new features
   - `Fix:` for bug fixes
   - `Update:` for updates to existing features
   - `Refactor:` for code refactoring
   - `Docs:` for documentation changes
   - `Test:` for test additions or changes

6. **Push to your fork**
   ```bash
   git push origin feature/your-feature-name
   ```

7. **Create a Pull Request**
   - Describe what your PR does
   - Reference any related issues (use `Closes #123` or `Fixes #123`)
   - Include screenshots if UI changes
   - Ensure all tests pass
   - Update CHANGELOG.md if applicable

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
   ```

3. Open solution in your IDE

4. Build the project:
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

