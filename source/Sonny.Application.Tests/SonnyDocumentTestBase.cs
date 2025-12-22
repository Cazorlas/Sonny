// Licensed to the.NET Foundation under one or more agreements.
// The.NET Foundation licenses this file to you under the MIT license.

using Autodesk.Revit.DB ;
using Autodesk.Revit.UI ;

namespace Sonny.Application.Tests ;

/// <summary>
///     Base class for tests that need a document
/// </summary>
public abstract class SonnyDocumentTestBase : SonnyRevitTestBase
{
    protected Document? Document { get ; private set ; }
    protected UIDocument? UIDocument { get ; private set ; }

    protected virtual string? DocumentFilePath => null ;

    protected override void OnSetup()
    {
        base.OnSetup() ;

        if (! string.IsNullOrEmpty(DocumentFilePath)) {
            UIDocument = OpenDocument(DocumentFilePath!)! ;
            Document = UIDocument.Document ;
        }

        AssertDocument(Document!,
            "Failed to create or open document") ;
    }

    protected override void OnTearDown()
    {
        UIApp?.OpenAndActivateDocument(GetTestRevitFilePath("PlaceHolder_V2023.rvt")) ;
        Document!.Close(false) ;
        Document.Dispose() ;

        base.OnTearDown() ;
    }
}
