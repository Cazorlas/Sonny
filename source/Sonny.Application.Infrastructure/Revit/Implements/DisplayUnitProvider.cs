using Sonny.Application.Domain.Entities.Settings ;
using Sonny.Application.Domain.Services ;
using Sonny.Application.Infrastructure.Revit.Services ;

namespace Sonny.Application.Infrastructure.Revit.Implements ;

/// <summary>
///     Provides default display unit based on document system
/// </summary>
public class DisplayUnitProvider(IRevitDocument revitDocument) : IDisplayUnitProvider
{
    public AppDisplayUnit GetDefaultDisplayUnit()
    {
        var document = revitDocument.Document ;
        var isMetric = document.DisplayUnitSystem == DisplayUnit.METRIC ;
        return isMetric ? AppDisplayUnit.Millimeters : AppDisplayUnit.Feet ;
    }
}
