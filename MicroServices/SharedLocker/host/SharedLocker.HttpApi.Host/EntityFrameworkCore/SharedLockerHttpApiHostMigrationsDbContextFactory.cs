using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SharedLocker.EntityFrameworkCore;

public class SharedLockerHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<SharedLockerHttpApiHostMigrationsDbContext>
{
    public SharedLockerHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<SharedLockerHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("SharedLocker"));

        return new SharedLockerHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
