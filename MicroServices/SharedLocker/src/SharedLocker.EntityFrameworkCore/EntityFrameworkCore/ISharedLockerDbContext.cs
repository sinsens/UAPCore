using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using SharedLocker.Domain.SharedLockers;
using SharedLocker.SharedLockers;

namespace SharedLocker.EntityFrameworkCore;

[ConnectionStringName(SharedLockerDbProperties.ConnectionStringName)]
public interface ISharedLockerDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<Locker> Lockers { get; set; }
    DbSet<LockerRent> LockerRents { get; set; }

    DbSet<LockerRentInfo> LockerRentInfos { get; set; }
}
