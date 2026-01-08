using Sonny.Application.Domain.Services ;

namespace Sonny.Application.UseCases.Services ;

public class LicenseCheckService(ILicenseValidator licenseValidator, IMessageService messageService)
    : ILicenseCheckService
{
    public bool CheckLicense()
    {
        var licenseStatus = licenseValidator.GetLicenseStatus() ;

        if (! licenseStatus.ExpiryDate.HasValue) {
            messageService.ShowError("License Required",
                "License invalid or not found") ;

            return false ;
        }

        // Check if license is expired first
        var isExpired = licenseStatus.ExpiryDate!.Value < DateTime.Now ;

        if (! isExpired) {
            return true ;
        }

        var expiryDate = licenseStatus.ExpiryDate.Value.ToString("yyyy-MM-dd") ;
        messageService.ShowError("License Expired",
            $"License has expired on {expiryDate}. Please renew your license.") ;

        return false ;
    }
}
