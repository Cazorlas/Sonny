using System.Windows ;
using System.Windows.Threading ;

namespace Sonny.Application.UI.Views ;

public partial class ProgressView : Window
{
    private readonly string _title ;

    public ProgressView(string title)
    {
        _title = title ;
        InitializeComponent() ;

        // Initialize progress bar
        ProgressBar.Minimum = 0 ;
        ProgressBar.Maximum = 100 ;
        ProgressBar.Value = 0 ;
    }

    /// <summary>
    ///     Updates progress bar
    /// </summary>
    public void UpdateProgress(int current,
        int total)
    {
        if (Dispatcher.CheckAccess())
        {
            ProgressBar.Maximum = total ;
            ProgressBar.Value = current ;
            Title = $"{_title} ({current} / {total})" ;
        }
        else
        {
            Dispatcher.Invoke(() =>
                {
                    ProgressBar.Maximum = total ;
                    ProgressBar.Value = current ;
                    Title = $"{_title} ({current} / {total})" ;
                },
                DispatcherPriority.Background) ;
        }
    }
}
