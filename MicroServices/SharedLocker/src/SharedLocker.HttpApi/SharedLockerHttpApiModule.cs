using Localization.Resources.AbpUi;
using SharedLocker.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace SharedLocker;

[DependsOn(
    typeof(SharedLockerApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class SharedLockerHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SharedLockerHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SharedLockerResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
