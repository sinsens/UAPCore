using Microsoft.EntityFrameworkCore;
using SharedLocker.Domain.SharedLockers;
using SharedLocker.SharedLockers;
using UAP.EntityFrameworkCore;
using Volo.Abp.Data;

namespace SharedLocker.EntityFrameworkCore;

[ConnectionStringName(SharedLockerDbProperties.ConnectionStringName)]
public class SharedLockerDbContext : UAPDbContextBase<SharedLockerDbContext>, ISharedLockerDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
        public DbSet<Locker> Lockers { get; set; }
        public DbSet<LockerRent> LockerRents { get; set; }
        public DbSet<LockerRentInfo> LockerRentInfos { get; set; }

    public SharedLockerDbContext(DbContextOptions<SharedLockerDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureSharedLocker();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging(true);
        optionsBuilder.LogTo(System.Console.WriteLine);
    }
}
