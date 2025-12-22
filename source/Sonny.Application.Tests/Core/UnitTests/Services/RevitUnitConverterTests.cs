using Autodesk.Revit.DB ;
using NUnit.Framework ;
using Sonny.Application.Core.Services ;

namespace Sonny.Application.Tests.Core.UnitTests.Services ;

/// <summary>
///     Unit tests for RevitUnitConverter
/// </summary>
[TestFixture]
public class RevitUnitConverterTests
{
    private RevitUnitConverter _converter ;

    [SetUp]
    public void Setup() => _converter = new RevitUnitConverter() ;

    [Test]
    public void ToInternalUnit_ShouldConvertMillimetersToFeet()
    {
        // Arrange
        var value = 3048.0 ; // 3048 mm = 10 feet
        var displayUnit = UnitTypeId.Millimeters ;

        // Act
        var result = _converter.ToInternalUnit(value,
            displayUnit) ;

        // Assert
        // 3048 mm = 10 feet (approximately)
        Assert.AreEqual(10.0,
            result,
            0.01) ;
    }

    [Test]
    public void ToInternalUnit_ShouldReturnSameValue_WhenAlreadyInFeet()
    {
        // Arrange
        var value = 10.0 ;
        var displayUnit = UnitTypeId.Feet ;

        // Act
        var result = _converter.ToInternalUnit(value,
            displayUnit) ;

        // Assert
        Assert.AreEqual(10.0,
            result) ;
    }

    [Test]
    public void FromInternalUnit_ShouldConvertFeetToMillimeters()
    {
        // Arrange
        var value = 10.0 ; // 10 feet
        var displayUnit = UnitTypeId.Millimeters ;

        // Act
        var result = _converter.FromInternalUnit(value,
            displayUnit) ;

        // Assert
        // 10 feet = 3048 mm (approximately)
        Assert.AreEqual(3048.0,
            result,
            0.01) ;
    }

    [Test]
    public void FromInternalUnit_ShouldReturnSameValue_WhenAlreadyInFeet()
    {
        // Arrange
        var value = 10.0 ;
        var displayUnit = UnitTypeId.Feet ;

        // Act
        var result = _converter.FromInternalUnit(value,
            displayUnit) ;

        // Assert
        Assert.AreEqual(10.0,
            result) ;
    }

    [Test]
    public void GetUnitDisplayName_ShouldReturnCorrectUnitNames()
    {
        // Act & Assert
        Assert.AreEqual("mm",
            _converter.GetUnitDisplayName(UnitTypeId.Millimeters)) ;
        Assert.AreEqual("cm",
            _converter.GetUnitDisplayName(UnitTypeId.Centimeters)) ;
        Assert.AreEqual("m",
            _converter.GetUnitDisplayName(UnitTypeId.Meters)) ;
        Assert.AreEqual("ft",
            _converter.GetUnitDisplayName(UnitTypeId.Feet)) ;
        Assert.AreEqual("in",
            _converter.GetUnitDisplayName(UnitTypeId.Inches)) ;
    }

    [Test]
    public void FormatWithUnit_ShouldFormatValueWithUnitSuffix()
    {
        // Arrange
        var value = 10.5 ;
        var displayUnit = UnitTypeId.Millimeters ;

        // Act
        var result = _converter.FormatWithUnit(value,
            displayUnit) ;

        // Assert
        Assert.IsTrue(result.Contains("10.50")) ;
        Assert.IsTrue(result.Contains("mm")) ;
    }

    [Test]
    public void ToInternalUnit_ShouldConvertInchesToFeet()
    {
        // Arrange
        var value = 120.0 ; // 120 inches = 10 feet
        var displayUnit = UnitTypeId.Inches ;

        // Act
        var result = _converter.ToInternalUnit(value,
            displayUnit) ;

        // Assert
        Assert.AreEqual(10.0,
            result,
            0.01) ;
    }

    [Test]
    public void FromInternalUnit_ShouldConvertFeetToInches()
    {
        // Arrange
        var value = 10.0 ; // 10 feet
        var displayUnit = UnitTypeId.Inches ;

        // Act
        var result = _converter.FromInternalUnit(value,
            displayUnit) ;

        // Assert
        Assert.AreEqual(120.0,
            result,
            0.01) ;
    }
}
