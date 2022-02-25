using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace SharedLocker;

[DependsOn(
    typeof(SharedLockerDomainModule),
    typeof(SharedLockerApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class SharedLockerApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<SharedLockerApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<SharedLockerApplicationModule>(validate: true);
        });
    }
}
