using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Volo.Abp.PermissionManagement;

namespace SharedLocker;

[DependsOn(
    typeof(SharedLockerDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
	typeof(AbpPermissionManagementApplicationContractsModule)
    )]
public class SharedLockerApplicationContractsModule : AbpModule
{

}
