using Sonny.Application.Presentation.ColumnFromCad.ViewModels ;
using Sonny.Application.Presentation.Extensions ;

namespace Sonny.Application.Presentation.ColumnFromCad.Views ;

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
