// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Autodesk.Revit.UI.Selection ;

namespace Sonny.Application.Core.SelectionFilters ;

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
