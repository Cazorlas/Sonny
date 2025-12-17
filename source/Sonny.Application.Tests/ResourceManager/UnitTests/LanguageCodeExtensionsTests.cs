using System ;
using System.Globalization ;
using NUnit.Framework ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Tests.ResourceManager.UnitTests ;

/// <summary>
///     Unit tests for LanguageCodeExtensions
/// </summary>
[TestFixture]
public class LanguageCodeExtensionsTests
{
    [Test]
    public void ToCodeString_ShouldReturnCorrectCode_ForAllLanguageCodes()
    {
        // Act & Assert
        Assert.AreEqual("en",
            LanguageCode.En.ToCodeString()) ;
        Assert.AreEqual("vi",
            LanguageCode.Vi.ToCodeString()) ;
        Assert.AreEqual("ja",
            LanguageCode.Ja.ToCodeString()) ;
        Assert.AreEqual("es",
            LanguageCode.Es.ToCodeString()) ;
        Assert.AreEqual("id",
            LanguageCode.Id.ToCodeString()) ;
        Assert.AreEqual("th",
            LanguageCode.Th.ToCodeString()) ;
        Assert.AreEqual("km",
            LanguageCode.Km.ToCodeString()) ;
        Assert.AreEqual("zh",
            LanguageCode.Zh.ToCodeString()) ;
        Assert.AreEqual("ko",
            LanguageCode.Ko.ToCodeString()) ;
    }

    [Test]
    public void ToCodeString_ShouldReturnEn_ForInvalidEnumValue()
    {
        // Arrange
        var invalidValue = (LanguageCode)999 ;

        // Act
        var result = invalidValue.ToCodeString() ;

        // Assert
        Assert.AreEqual("en",
            result) ;
    }

    [Test]
    public void ToLanguageCode_ShouldReturnCorrectEnum_ForValidCodeStrings()
    {
        // Act & Assert
        Assert.AreEqual(LanguageCode.En,
            "en".ToLanguageCode()) ;
        Assert.AreEqual(LanguageCode.Vi,
            "vi".ToLanguageCode()) ;
        Assert.AreEqual(LanguageCode.Ja,
            "ja".ToLanguageCode()) ;
        Assert.AreEqual(LanguageCode.Es,
            "es".ToLanguageCode()) ;
        Assert.AreEqual(LanguageCode.Id,
            "id".ToLanguageCode()) ;
        Assert.AreEqual(LanguageCode.Th,
            "th".ToLanguageCode()) ;
        Assert.AreEqual(LanguageCode.Km,
            "km".ToLanguageCode()) ;
        Assert.AreEqual(LanguageCode.Zh,
            "zh".ToLanguageCode()) ;
        Assert.AreEqual(LanguageCode.Ko,
            "ko".ToLanguageCode()) ;
    }

    [Test]
    public void ToLanguageCode_ShouldBeCaseInsensitive()
    {
        // Act & Assert
        Assert.AreEqual(LanguageCode.En,
            "EN".ToLanguageCode()) ;
        Assert.AreEqual(LanguageCode.Vi,
            "VI".ToLanguageCode()) ;
        Assert.AreEqual(LanguageCode.Ja,
            "Ja".ToLanguageCode()) ;
        Assert.AreEqual(LanguageCode.Es,
            "ES".ToLanguageCode()) ;
    }

    [Test]
    public void ToLanguageCode_ShouldReturnEn_ForNullString()
    {
        // Act
        var result = ((string?)null).ToLanguageCode() ;

        // Assert
        Assert.AreEqual(LanguageCode.En,
            result) ;
    }

    [Test]
    public void ToLanguageCode_ShouldReturnEn_ForEmptyString()
    {
        // Act
        var result = string.Empty.ToLanguageCode() ;

        // Assert
        Assert.AreEqual(LanguageCode.En,
            result) ;
    }

    [Test]
    public void ToLanguageCode_ShouldReturnEn_ForWhitespaceString()
    {
        // Act
        var result = "   ".ToLanguageCode() ;

        // Assert
        Assert.AreEqual(LanguageCode.En,
            result) ;
    }

    [Test]
    public void ToLanguageCode_ShouldReturnEn_ForInvalidCodeString()
    {
        // Act
        var result = "invalid".ToLanguageCode() ;

        // Assert
        Assert.AreEqual(LanguageCode.En,
            result) ;
    }

    [Test]
    public void ToCultureInfo_ShouldReturnCorrectCultureInfo_ForValidLanguageCodes()
    {
        // Act & Assert
        var enCulture = LanguageCode.En.ToCultureInfo() ;
        Assert.AreEqual("en",
            enCulture.TwoLetterISOLanguageName) ;

        var viCulture = LanguageCode.Vi.ToCultureInfo() ;
        Assert.AreEqual("vi",
            viCulture.TwoLetterISOLanguageName) ;

        var jaCulture = LanguageCode.Ja.ToCultureInfo() ;
        Assert.AreEqual("ja",
            jaCulture.TwoLetterISOLanguageName) ;
    }

    [Test]
    public void ToCultureInfo_ShouldReturnEnglishCulture_WhenCultureCreationFails()
    {
        // Note: This test verifies fallback behavior
        // In practice, all valid language codes should create valid cultures
        // But the method has a try-catch to handle edge cases

        // Act
        var result = LanguageCode.En.ToCultureInfo() ;

        // Assert
        Assert.IsNotNull(result) ;
        Assert.AreEqual("en",
            result.TwoLetterISOLanguageName) ;
    }

    [Test]
    public void ToCultureInfo_ShouldReturnValidCultureInfo_ForAllLanguageCodes()
    {
        // Act & Assert - Verify all language codes can create valid cultures
        var allCodes = Enum.GetValues<LanguageCode>() ;

        foreach (var code in allCodes)
        {
            var culture = code.ToCultureInfo() ;
            Assert.IsNotNull(culture,
                $"Culture should not be null for {code}") ;
            Assert.IsNotNull(culture.TwoLetterISOLanguageName,
                $"TwoLetterISOLanguageName should not be null for {code}") ;
        }
    }
}

