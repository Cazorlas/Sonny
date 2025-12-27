using System.Windows ;
using Sonny.Application.Presentation.Settings.ViewModels ;
using Sonny.Application.Presentation.Extensions ;

namespace Sonny.Application.Presentation.Settings.Views ;

/// <summary>
///     Interaction logic for SettingsView.xaml
/// </summary>
public partial class SettingsView : Window
{
    /// <summary>
    ///     Initializes a new instance of SettingsView
    /// </summary>
    /// <param name="viewModel">The view model</param>
    public SettingsView(SettingsViewModel viewModel)
    {
        InitializeComponent() ;
        this.SetOwnerByRevit() ;
        DataContext = viewModel ;

        // Set close window action
        viewModel.Window = this ;
    }
}
