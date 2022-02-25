using SharedLocker.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace SharedLocker;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(SharedLockerEntityFrameworkCoreTestModule)
    )]
public class SharedLockerDomainTestModule : AbpModule
{

}
