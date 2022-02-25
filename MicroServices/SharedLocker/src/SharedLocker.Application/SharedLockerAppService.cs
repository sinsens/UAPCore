using SharedLocker.Localization;
using Volo.Abp.Application.Services;

namespace SharedLocker;

public abstract class SharedLockerAppService : ApplicationService
{
    protected SharedLockerAppService()
    {
        LocalizationResource = typeof(SharedLockerResource);
        ObjectMapperContext = typeof(SharedLockerApplicationModule);
    }
}
