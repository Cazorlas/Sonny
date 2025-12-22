using System ;
using NUnit.Framework ;
using Sonny.Application.Features.Settings.Models ;
using Sonny.Application.Tests.Utils ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Tests.ResourceManager.UnitTests ;

/// <summary>
///     Unit tests for LanguageOption
/// </summary>
[TestFixture]
public class LanguageOptionTests
{
    [Test]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var displayName = "English" ;
        var languageCode = LanguageCode.En ;

        // Act
        var option = new LanguageOption(displayName,
            languageCode) ;

        // Assert
        Assert.AreEqual(displayName,
            option.DisplayName) ;
        Assert.AreEqual(languageCode,
            option.LanguageCode) ;
    }

    [Test]
    public void Constructor_ShouldHandleEmptyDisplayName()
    {
        // Arrange
        var displayName = string.Empty ;
        var languageCode = LanguageCode.En ;

        // Act
        var option = new LanguageOption(displayName,
            languageCode) ;

        // Assert
        Assert.AreEqual(string.Empty,
            option.DisplayName) ;
        Assert.AreEqual(LanguageCode.En,
            option.LanguageCode) ;
    }

    [Test]
    public void Constructor_ShouldHandleNullDisplayName()
    {
        // Arrange
        string? displayName = null ;
        var languageCode = LanguageCode.Vi ;

        // Act
        var option = new LanguageOption(displayName!,
            languageCode) ;

        // Assert
        Assert.IsNull(option.DisplayName) ;
        Assert.AreEqual(LanguageCode.Vi,
            option.LanguageCode) ;
    }

    [Test]
    public void ToString_ShouldReturnDisplayName()
    {
        // Arrange
        var displayName = "Vietnamese" ;
        var languageCode = LanguageCode.Vi ;
        var option = new LanguageOption(displayName,
            languageCode) ;

        // Act
        var result = option.ToString() ;

        // Assert
        Assert.AreEqual(displayName,
            result) ;
    }

    [Test]
    public void ToString_ShouldReturnEmptyString_WhenDisplayNameIsEmpty()
    {
        // Arrange
        var option = new LanguageOption(string.Empty,
            LanguageCode.En) ;

        // Act
        var result = option.ToString() ;

        // Assert
        Assert.AreEqual(string.Empty,
            result) ;
    }

    [Test]
    public void Properties_ShouldBeReadOnly()
    {
        // Arrange
        var option = new LanguageOption("English",
            LanguageCode.En) ;

        // Act & Assert
        // Properties should be get-only, so we can't set them
        // This test verifies the properties exist and are accessible
        Assert.IsNotNull(option.DisplayName) ;
        Assert.IsNotNull(option.LanguageCode) ;
    }

    [Test]
    public void Constructor_ShouldWorkWithAllLanguageCodes()
    {
        // Act & Assert - Verify constructor works with all language codes
        var allCodes = EnumHelper.GetValues<LanguageCode>() ;

        foreach (var code in allCodes) {
            var displayName = $"Language {code}" ;
            var option = new LanguageOption(displayName,
                code) ;

            Assert.AreEqual(displayName,
                option.DisplayName) ;
            Assert.AreEqual(code,
                option.LanguageCode) ;
        }
    }
}
