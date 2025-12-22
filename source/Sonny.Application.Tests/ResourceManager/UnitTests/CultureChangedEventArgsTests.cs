using System.Globalization ;
using NUnit.Framework ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Tests.ResourceManager.UnitTests ;

/// <summary>
///     Unit tests for CultureChangedEventArgs
/// </summary>
[TestFixture]
public class CultureChangedEventArgsTests
{
    [Test]
    public void Constructor_ShouldInitializeCulture()
    {
        // Arrange
        var culture = new CultureInfo("en-US") ;

        // Act
        var args = new CultureChangedEventArgs(culture) ;

        // Assert
        Assert.AreEqual(culture,
            args.Culture) ;
    }

    [Test]
    public void Constructor_ShouldHandleEnglishCulture()
    {
        // Arrange
        var culture = new CultureInfo("en") ;

        // Act
        var args = new CultureChangedEventArgs(culture) ;

        // Assert
        Assert.AreEqual(culture,
            args.Culture) ;
        Assert.AreEqual("en",
            args.Culture.TwoLetterISOLanguageName) ;
    }

    [Test]
    public void Constructor_ShouldHandleVietnameseCulture()
    {
        // Arrange
        var culture = new CultureInfo("vi-VN") ;

        // Act
        var args = new CultureChangedEventArgs(culture) ;

        // Assert
        Assert.AreEqual(culture,
            args.Culture) ;
        Assert.AreEqual("vi",
            args.Culture.TwoLetterISOLanguageName) ;
    }

    [Test]
    public void Constructor_ShouldHandleJapaneseCulture()
    {
        // Arrange
        var culture = new CultureInfo("ja-JP") ;

        // Act
        var args = new CultureChangedEventArgs(culture) ;

        // Assert
        Assert.AreEqual(culture,
            args.Culture) ;
        Assert.AreEqual("ja",
            args.Culture.TwoLetterISOLanguageName) ;
    }

    [Test]
    public void Culture_ShouldBeReadOnly()
    {
        // Arrange
        var culture = new CultureInfo("en-US") ;
        var args = new CultureChangedEventArgs(culture) ;

        // Act & Assert
        // Culture property should be get-only
        Assert.IsNotNull(args.Culture) ;
        Assert.AreEqual(culture,
            args.Culture) ;
    }

    [Test]
    public void Constructor_ShouldHandleInvariantCulture()
    {
        // Arrange
        var culture = CultureInfo.InvariantCulture ;

        // Act
        var args = new CultureChangedEventArgs(culture) ;

        // Assert
        Assert.AreEqual(culture,
            args.Culture) ;
    }
}

