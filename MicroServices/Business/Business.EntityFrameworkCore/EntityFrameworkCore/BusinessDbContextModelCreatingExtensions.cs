using Business.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Business.EntityFrameworkCore
{
    public static class BusinessDbContextModelCreatingExtensions
    {
        public static void ConfigureBusiness(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Book>(b =>
            {
                b.ToTable("Book");

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired();
            });
            /*
            builder.Entity<App>(b =>
            {

                b.ToTable("Apps");

                b.ConfigureByConvention();

                b.Property(x => x.AppId).HasMaxLength(AppConsts.MaxAppIdLength).IsRequired();
                b.Property(x => x.AppName).HasMaxLength(AppConsts.MaxAppNameLength).IsRequired();
                b.Property(x => x.AppSecret).HasMaxLength(AppConsts.MaxAppSecretLength);

                b.HasIndex(x => x.AppId);
                b.HasIndex(x => x.AppName);
                b.HasIndex(x => x.AppType);
                b.HasIndex(x => x.CreationTime);

            });

            builder.Entity<AppUser>(b =>
            {
                b.ToTable("AppUsers");

                b.Property(x => x.Address).HasMaxLength(AppUserConsts.MaxAddressLength);
                b.Property(x => x.Phone).HasMaxLength(AppUserConsts.MaxPhoneLength);
                b.Property(x => x.City).HasMaxLength(AppUserConsts.MaxCityLength);
                b.Property(x => x.Name).HasMaxLength(AppUserConsts.MaxNameLength);
                b.Property(x => x.OpenId).HasMaxLength(AppUserConsts.MaxOpenIdLength).IsRequired();

                b.ConfigureByConvention();

                b.HasIndex(x => x.OpenId);
                b.HasIndex(x => x.Name);
                b.HasIndex(x => x.Phone);
                b.HasIndex(x => x.City);
                b.HasIndex(x => x.Gender);
                b.HasIndex(x => x.CreationTime);
            });
            */
            //Code generation...
        }
    }
}
