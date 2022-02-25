using Microsoft.Extensions.DependencyInjection;
using UAP.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace SharedLocker.EntityFrameworkCore;

[DependsOn(
    typeof(SharedLockerDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(UAPEntityFrameworkCoreModule)
)]
public class SharedLockerEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<SharedLockerDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddDefaultRepositories(true);
        });
    }
}
