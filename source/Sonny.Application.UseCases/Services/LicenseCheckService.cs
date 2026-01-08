using Sonny.Application.Domain.Services ;

namespace Sonny.Application.UseCases.Services ;

public class LicenseCheckService(ILicenseValidator licenseValidator, IMessageService messageService)
    : ILicenseCheckService
{
    public bool CheckLicense()
    {
        var licenseStatus = licenseValidator.GetLicenseStatus() ;

        // Check if license is invalid or expired
        var isExpired = licenseStatus.ExpiryDate.HasValue && licenseStatus.ExpiryDate.Value < DateTime.Now ;

        if (licenseStatus.IsValid
            && ! isExpired) {
            return true ;
        }

        try {
            string errorMessage ;

            if (licenseStatus.ExpiryDate != null) {
                errorMessage =
                    $"License has expired on {licenseStatus.ExpiryDate.Value:yyyy-MM-dd}. Please renew your license to continue." ;
            }
            else {
                errorMessage = string.IsNullOrEmpty(licenseStatus.Error)
                    ? "License is not valid. Please activate your license to continue."
                    : $"License is not valid: {licenseStatus.Error}" ;
            }

            messageService.ShowWarning("License Required",
                errorMessage) ;

            licenseValidator.ShowLicenseWindow() ;
        }
        catch {
            // Ignore message service errors
        }

        return false ;
    }
}
