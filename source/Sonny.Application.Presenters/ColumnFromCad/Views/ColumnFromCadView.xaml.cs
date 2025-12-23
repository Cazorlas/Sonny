using Sonny.Application.Presenters.Extensions ;
using Sonny.Application.Presenters.ColumnFromCad.ViewModels ;

namespace Sonny.Application.Presenters.ColumnFromCad.Views ;

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
