using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using UAP.Domain;

namespace SharedLocker;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(SharedLockerDomainSharedModule),
	typeof(UAPDomainModule)
)]
public class SharedLockerDomainModule : AbpModule
{

}
