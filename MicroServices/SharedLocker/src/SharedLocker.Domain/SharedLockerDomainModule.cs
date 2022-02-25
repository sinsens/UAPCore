using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace SharedLocker;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(SharedLockerDomainSharedModule)
)]
public class SharedLockerDomainModule : AbpModule
{

}
