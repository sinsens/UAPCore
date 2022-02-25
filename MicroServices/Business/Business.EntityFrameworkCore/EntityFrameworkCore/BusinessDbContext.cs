using Microsoft.EntityFrameworkCore;
using UAP.EntityFrameworkCore;
using Volo.Abp.Data;

namespace Business.EntityFrameworkCore
{
    [ConnectionStringName("Business")]
    public class BusinessDbContext : UAPDbContextBase<BusinessDbContext>
    {
        //Code generation...

        /*
        public DbSet<App> Apps { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        */

        public BusinessDbContext(DbContextOptions<BusinessDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureBusiness();
        }
    }
}
