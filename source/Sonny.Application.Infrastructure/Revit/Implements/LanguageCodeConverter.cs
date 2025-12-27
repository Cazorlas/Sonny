using Sonny.Application.Domain.Entities.Settings ;
using Sonny.ResourceManager ;

namespace Sonny.Application.Infrastructure.Revit.Implements ;

public static class LanguageCodeConverter
{
    public static LanguageCode ToResourceManagerLanguageCode(AppLanguageCode appLanguageCode) =>
        appLanguageCode switch
        {
            AppLanguageCode.En => LanguageCode.En,
            AppLanguageCode.Vi => LanguageCode.Vi,
            AppLanguageCode.Ja => LanguageCode.Ja,
            AppLanguageCode.Es => LanguageCode.Es,
            AppLanguageCode.Id => LanguageCode.Id,
            AppLanguageCode.Th => LanguageCode.Th,
            AppLanguageCode.Km => LanguageCode.Km,
            AppLanguageCode.Zh => LanguageCode.Zh,
            AppLanguageCode.Ko => LanguageCode.Ko,
            _ => LanguageCode.En
        } ;

    public static AppLanguageCode FromResourceManagerLanguageCode(LanguageCode resourceManagerLanguageCode) =>
        resourceManagerLanguageCode switch
        {
            LanguageCode.En => AppLanguageCode.En,
            LanguageCode.Vi => AppLanguageCode.Vi,
            LanguageCode.Ja => AppLanguageCode.Ja,
            LanguageCode.Es => AppLanguageCode.Es,
            LanguageCode.Id => AppLanguageCode.Id,
            LanguageCode.Th => AppLanguageCode.Th,
            LanguageCode.Km => AppLanguageCode.Km,
            LanguageCode.Zh => AppLanguageCode.Zh,
            LanguageCode.Ko => AppLanguageCode.Ko,
            _ => AppLanguageCode.En
        } ;
}
