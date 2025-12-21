using Sonny.Application.Core.RevitExtensions ;
using Sonny.Application.Features.ColumnFromCad.ViewModels ;

namespace Sonny.Application.Features.ColumnFromCad.Views ;

public partial class ColumnFromCadView
{
    public ColumnFromCadView(ColumnFromCadViewModel viewModel)
    {
        InitializeComponent() ;
        this.SetOwnerByRevit() ;
        DataContext = viewModel ;

        // Set close window action
        viewModel.Window = this ;
    }
}
