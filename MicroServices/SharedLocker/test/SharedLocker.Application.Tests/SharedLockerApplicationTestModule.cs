using Volo.Abp.Modularity;

namespace SharedLocker;

[DependsOn(
    typeof(SharedLockerApplicationModule),
    typeof(SharedLockerDomainTestModule)
    )]
public class SharedLockerApplicationTestModule : AbpModule
{

}
