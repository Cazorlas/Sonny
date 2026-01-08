namespace Sonny.Application.Domain.Entities.License ;

public class LicenseStatus
{
    public bool IsValid { get ; set ; }

    public string? Email { get ; set ; }

    /// <summary>
    ///     User ID from Keygen
    /// </summary>
    public string? UserId { get ; set ; }

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
        string? userId,
        string? licenseType,
        DateTime? startDate,
        DateTime? expiryDate) =>
        new()
        {
            IsValid = true,
            Email = email,
            UserId = userId,
            LicenseType = licenseType,
            StartDate = startDate,
            ExpiryDate = expiryDate
        } ;
}
