using System.Globalization ;
using WPFLocalizeExtension.Engine ;

namespace Sonny.ResourceManager ;

/// <summary>
///     Manages culture and language settings, integrates with WPFLocalizeExtension
/// </summary>
/// <remarks>
///     This class is responsible for managing culture information and integrating
///     with WPFLocalizeExtension library. It follows Single Responsibility Principle
///     by focusing solely on culture/language management.
/// </remarks>
internal class CultureManager
{
    private CultureInfo _currentCulture = CultureInfo.CurrentCulture ;

    /// <summary>
    ///     Initializes a new instance of CultureManager
    /// </summary>
    public CultureManager() => InitializeLocalizeExtension() ;

    /// <summary>
    ///     Gets or sets the current culture
    /// </summary>
    public CultureInfo CurrentCulture {
        get => _currentCulture ;
        set
        {
            if (_currentCulture.Equals(value)) {
                return ;
            }

            _currentCulture = value ;
            UpdateLocalizeExtensionCulture() ;
        }
    }

    /// <summary>
    ///     Gets the current language code as string (e.g., "en", "vi", "ja")
    /// </summary>
    public string CurrentLanguageCode => _currentCulture.TwoLetterISOLanguageName ;

    /// <summary>
    ///     Gets the current language code as LanguageCode enum
    /// </summary>
    public LanguageCode CurrentLanguageCodeEnum => CurrentLanguageCode.ToLanguageCode() ;

    /// <summary>
    ///     Event raised when culture is changed
    /// </summary>
    public event EventHandler<CultureChangedEventArgs>? CultureChanged ;

    /// <summary>
    ///     Change language using LanguageCode enum
    /// </summary>
    /// <param name="languageCode">Language code enum</param>
    public void ChangeLanguage(LanguageCode languageCode) => CurrentCulture = languageCode.ToCultureInfo() ;

    /// <summary>
    ///     Change language using CultureInfo
    /// </summary>
    /// <param name="culture">Culture to set</param>
    public void ChangeLanguage(CultureInfo culture) =>
        CurrentCulture = culture ?? throw new ArgumentNullException(nameof( culture )) ;

    /// <summary>
    ///     Initialize WPFLocalizeExtension if available
    /// </summary>
    private void InitializeLocalizeExtension()
    {
        try {
            LocalizeDictionary.Instance.Culture = _currentCulture ;
        }
        catch {
            // WPFLocalizeExtension not available or failed to initialize
            // Continue without it
        }
    }

    /// <summary>
    ///     Update WPFLocalizeExtension culture
    /// </summary>
    private void UpdateLocalizeExtensionCulture()
    {
        try {
            LocalizeDictionary.Instance.Culture = _currentCulture ;
            CultureChanged?.Invoke(this,
                new CultureChangedEventArgs(_currentCulture)) ;
        }
        catch {
            // WPFLocalizeExtension not available
        }
    }
}
