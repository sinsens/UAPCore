using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace SharedLocker;

[DependsOn(
    typeof(SharedLockerApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class SharedLockerHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(SharedLockerApplicationContractsModule).Assembly,
            SharedLockerRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SharedLockerHttpApiClientModule>();
        });

    }
}
