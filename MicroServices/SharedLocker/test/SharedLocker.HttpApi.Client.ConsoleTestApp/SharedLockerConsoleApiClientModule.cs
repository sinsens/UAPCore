using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace SharedLocker;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SharedLockerHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class SharedLockerConsoleApiClientModule : AbpModule
{

}
