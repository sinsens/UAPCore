using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SharedLocker.EntityFrameworkCore;

public class SharedLockerHttpApiHostMigrationsDbContext : AbpDbContext<SharedLockerHttpApiHostMigrationsDbContext>
{
    public SharedLockerHttpApiHostMigrationsDbContext(DbContextOptions<SharedLockerHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureSharedLocker();
    }
}
