using SharedLocker.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace SharedLocker;

public abstract class SharedLockerController : AbpControllerBase
{
    protected SharedLockerController()
    {
        LocalizationResource = typeof(SharedLockerResource);
    }
}
