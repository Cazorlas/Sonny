using System ;
using System.Collections.Generic ;
using Autodesk.Revit.DB ;
using NSubstitute ;
using NUnit.Framework ;
using Serilog ;
using Sonny.Application.Core.Interfaces ;
using Sonny.Application.Features.AutoColumnDimension.Interfaces ;
using Sonny.Application.Features.AutoColumnDimension.ViewModels ;

namespace Sonny.Application.Tests.Features.AutoColumnDimension.UnitTests ;

/// <summary>
///     Unit tests for AutoColumnDimensionViewModel
///     Tests ViewModel logic without requiring Revit API context
/// </summary>
[TestFixture]
public class AutoColumnDimensionViewModelTests
{
    [SetUp]
    public void Setup()
    {
        // Create mocks using NSubstitute
        _handler = Substitute.For<IAutoColumnDimensionHandler>() ;
        _revitDocument = Substitute.For<IRevitDocument>() ;
        _messageService = Substitute.For<IMessageService>() ;
        _logger = Substitute.For<ILogger>() ;
        _unitConverter = Substitute.For<IUnitConverter>() ;

        // Setup common services mock
        _commonServices = Substitute.For<ICommonServices>() ;
        _commonServices.RevitDocument.Returns(_revitDocument) ;
        _commonServices.MessageService.Returns(_messageService) ;
        _commonServices.Logger.Returns(_logger) ;
        _commonServices.UnitConverter.Returns(_unitConverter) ;

        // Setup default display unit (millimeters)
        // Note: GetDefaultDisplayUnit receives Document from RevitDocument.Document
        // We don't need to mock Document directly, just mock the method call
        var displayUnit = UnitTypeId.Millimeters ;
        _unitConverter.GetDefaultDisplayUnit(Arg.Any<Document>())
            .Returns(displayUnit) ;
        _unitConverter.GetUnitDisplayName(displayUnit)
            .Returns("mm") ;

        _revitDocument.Document.Returns((Document?)null) ;
    }

    private ICommonServices _commonServices ;
    private IAutoColumnDimensionHandler _handler ;
    private IRevitDocument _revitDocument ;
    private IMessageService _messageService ;
    private ILogger _logger ;
    private IUnitConverter _unitConverter ;
    private AutoColumnDimensionViewModel _viewModel ;

    [Test]
    public void Constructor_ShouldInitializeViewModel()
    {
        // Arrange
        var dimensionTypes = new List<DimensionType>() ;
        _revitDocument.GetDimensionTypes()
            .Returns(dimensionTypes) ;

        // Act
        _viewModel = new AutoColumnDimensionViewModel(_commonServices,
            _handler) ;

        // Assert
        Assert.IsNotNull(_viewModel) ;
        Assert.IsNotNull(_viewModel.DimensionTypes) ;
        Assert.AreEqual(0,
            _viewModel.DimensionTypes.Count) ;
    }

    [Test]
    public void Constructor_WhenNoDimensionTypes_ShouldSetSelectedDimensionTypeToNull()
    {
        // Arrange
        var dimensionTypes = new List<DimensionType>() ;
        _revitDocument.GetDimensionTypes()
            .Returns(dimensionTypes) ;

        // Act
        _viewModel = new AutoColumnDimensionViewModel(_commonServices,
            _handler) ;

        // Assert
        Assert.IsNull(_viewModel.SelectedDimensionType) ;
    }

    [Test]
    public void Constructor_WhenGetDimensionTypesThrowsException_ShouldLogError()
    {
        // Arrange
        var exception = new Exception("Test exception") ;
        _revitDocument.GetDimensionTypes()
            .Returns(x => throw exception) ;

        // Act
        _viewModel = new AutoColumnDimensionViewModel(_commonServices,
            _handler) ;

        // Assert
        _logger.Received()
            .Error(Arg.Is<Exception>(e => e.Message == "Test exception"),
                Arg.Is<string>(s => s.Contains("Failed to initialize dimension types"))) ;
        _messageService.Received()
            .ShowError(Arg.Is<string>(s => s.Contains("Failed to initialize dimension types"))) ;
    }
}
