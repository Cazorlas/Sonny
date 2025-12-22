using System.Windows ;
using Sonny.Application.Features.AutoColumnDimension.ViewModels ;
using Sonny.Application.Infrastructure.Extensions ;

namespace Sonny.Application.Features.AutoColumnDimension.Views ;

/// <summary>
///     Interaction logic for AutoColumnDimensionView.xaml
/// </summary>
public partial class AutoColumnDimensionView : Window
{
    /// <summary>
    ///     Initializes a new instance of AutoColumnDimensionView
    /// </summary>
    /// <param name="viewModel">The view model</param>
    public AutoColumnDimensionView(AutoColumnDimensionViewModel viewModel)
    {
        InitializeComponent() ;
        this.SetOwnerByRevit() ;
        DataContext = viewModel ;

        // Set close window action
        viewModel.Window = this ;
    }
}
