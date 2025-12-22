using Sonny.Application.Features.ColumnFromCad.ViewModels ;
using Sonny.Application.Infrastructure.Extensions ;

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
