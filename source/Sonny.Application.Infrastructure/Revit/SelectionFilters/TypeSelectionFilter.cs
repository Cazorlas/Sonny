using Autodesk.Revit.UI.Selection ;

namespace Sonny.Application.Infrastructure.Revit.SelectionFilters ;

public class TypeSelectionFilter : ISelectionFilter
{
    private readonly List<Guid> _typeGuid = [] ;

    public TypeSelectionFilter(Type type) => _typeGuid.Add(type.GUID) ;

    public TypeSelectionFilter(List<Type> types) =>
        _typeGuid = types.Select(category => category.GUID)
            .ToList() ;

    public bool AllowElement(Element elem) =>
        _typeGuid.Contains(elem.GetType()
            .GUID) ;

    public bool AllowReference(Reference reference,
        XYZ position) =>
        false ;
}
