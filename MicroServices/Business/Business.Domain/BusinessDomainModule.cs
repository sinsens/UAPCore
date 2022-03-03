using Volo.Abp.AuditLogging;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using UAP.Domain;

namespace Business
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpSettingManagementDomainModule),
		typeof(UAPDomainModule)
    )]
    public class BusinessDomainModule : AbpModule
    {

    }
}
