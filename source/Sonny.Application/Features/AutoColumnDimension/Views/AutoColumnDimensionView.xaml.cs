using System.Windows ;
using Sonny.Application.Features.AutoColumnDimension.ViewModels ;
using Sonny.Application.RevitExtensions ;

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
        ViewModel = viewModel ;
        DataContext = ViewModel ;

        // Set close window action
        ViewModel.CloseWindowAction = ExecuteOk ;
    }

    /// <summary>
    ///     The view model
    /// </summary>
    public AutoColumnDimensionViewModel ViewModel { get ; }

    /// <summary>
    ///     Executes OK action
    /// </summary>
    public void ExecuteOk() =>
        // Note: DialogResult can only be set for modal dialogs (ShowDialog)
        // Since we use Show() for non-modal window (to support Revit.Async),
        // we only close the window without setting DialogResult
        Close() ;
}
