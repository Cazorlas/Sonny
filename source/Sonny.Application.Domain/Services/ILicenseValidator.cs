using Sonny.Application.Domain.Entities.License ;

namespace Sonny.Application.Domain.Services ;

public interface ILicenseValidator
{
    LicenseStatus GetLicenseStatus() ;
    Task<LicenseStatus> TryAutoLoginAsync() ;
    Task ShowLicenseWindow() ;
}
