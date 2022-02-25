using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using UAP.Domain;

namespace UAP.EntityFrameworkCore;

[DependsOn(
	typeof(UAPDomainModule),
	typeof(AbpEntityFrameworkCoreModule)
	)]
public class UAPEntityFrameworkCoreModule : AbpModule
{

}
