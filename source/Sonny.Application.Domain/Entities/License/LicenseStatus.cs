namespace Sonny.Application.Domain.Entities.License ;

public class LicenseStatus
{
    public bool IsValid { get ; set ; }
    public string? Email { get ; set ; }
    public bool IsOfflineLicense { get ; set ; }

    /// <summary>
    ///     License type/policy name (e.g., "Trial", "Pro", etc.)
    /// </summary>
    public string? LicenseType { get ; set ; }

    public DateTime? StartDate { get ; set ; }
    public DateTime? ExpiryDate { get ; set ; }

    /// <summary>
    ///     Error message if license is invalid
    /// </summary>
    public string? Error { get ; set ; }

    public static LicenseStatus Invalid(string? error = null) => new() { IsValid = false, Error = error } ;

    public static LicenseStatus Valid(string? email,
        string? licenseType,
        bool isOfflineLicense,
        DateTime? startDate,
        DateTime? expiryDate) =>
        new()
        {
            IsValid = true,
            Email = email,
            LicenseType = licenseType,
            IsOfflineLicense = isOfflineLicense,
            StartDate = startDate,
            ExpiryDate = expiryDate
        } ;
}
