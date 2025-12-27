using Sonny.Application.Domain.Services ;
using Sonny.Application.Infrastructure.Revit.Services ;

namespace Sonny.Application.Infrastructure.Revit.Implements ;

/// <summary>
///     Provides view scale information
/// </summary>
public class ViewScaleProvider(IRevitDocument revitDocument) : IViewScaleProvider
{
    public double GetActiveViewScale() => revitDocument.ActiveView.Scale ;
}
