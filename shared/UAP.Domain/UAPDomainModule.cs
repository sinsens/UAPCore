using Microsoft.Extensions.DependencyInjection.Extensions;
using UAP.Domain.Volo.Abp.Users;
using Volo.Abp.Modularity;
using Volo.Abp.Security;
using Volo.Abp.Users;

namespace UAP.Domain
{
    [DependsOn(typeof(AbpSecurityModule))]
    public class UAPDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
            context.Services.Replace(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(typeof(ICurrentUser), typeof(UAPCurrentUser), Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient));
        }
    }
}
