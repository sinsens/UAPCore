using SharedLocker.Localization;
using UAP.Shared;
using Volo.Abp.Application.Services;

namespace SharedLocker;

public abstract class SharedLockerAppService : ApplicationService
{
    protected ICurrentApp currentApp;
    protected SharedLockerAppService(ICurrentApp currentApp)
    {
        this.currentApp = currentApp;
        LocalizationResource = typeof(SharedLockerResource);
        ObjectMapperContext = typeof(SharedLockerApplicationModule);
    }
}
