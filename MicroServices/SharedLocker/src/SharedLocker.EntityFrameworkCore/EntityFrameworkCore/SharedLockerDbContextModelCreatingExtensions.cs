using SharedLocker.Domain.SharedLockers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using SharedLocker.Consts;
using System.Linq;
using SharedLocker.SharedLockers;

namespace SharedLocker.EntityFrameworkCore;

public static class SharedLockerDbContextModelCreatingExtensions
{
    public static void ConfigureSharedLocker(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Locker>(b =>
        {
            b.ToTable(SharedLockerDbProperties.DbTablePrefix + "Lockers", SharedLockerDbProperties.DbSchema);
            b.ConfigureByConvention();


                /* Configure more properties here */

            b.Property(x => x.Name).HasMaxLength(LockerConst.MaxLockerNameLength);
            b.HasMany(x => x.RentInfos).WithOne(c => c.Locker);

            b.HasIndex(x => x.Name);
            b.HasIndex(x => x.Number);
            b.HasIndex(x => x.Status);
            b.HasIndex(x => x.IsActive);
            b.HasIndex(x => x.AppId);
            b.HasIndex(x => x.IsDeleted);
        });

        builder.Entity<LockerRent>(b =>
        {
            b.ToTable(SharedLockerDbProperties.DbTablePrefix + "LockerRents", SharedLockerDbProperties.DbSchema);
            b.ConfigureByConvention();


                /* Configure more properties here */

            b.Property(x => x.Name).HasMaxLength(LockerRentConst.MaxLockerRentNameLength);
            b.Property(x => x.Phone).HasMaxLength(LockerRentConst.MaxLockerRentPhoneLength);
            b.Property(x => x.Remark).HasMaxLength(LockerRentConst.MaxLockerRentRemarkLength);
            b.Property(x => x.ReturnRemark).HasMaxLength(LockerRentConst.MaxLockerRentReturnRemarkLength);
            b.Property(x => x.PinyinName).HasMaxLength(LockerRentConst.MaxLockerRentPinyinNameLength);
            b.Property(x => x.FullPinyinName).HasMaxLength(LockerRentConst.MaxLockerRentFullPinyinNameLength);
            b.HasMany(x => x.Logs).WithOne(log => log.LockerRent);
            b.HasMany(x => x.RentInfos).WithOne(c => c.LockerRent);

            b.HasIndex(x => x.Name);
            b.HasIndex(x => x.PinyinName);
            b.HasIndex(x => x.Phone);
            b.HasIndex(x => x.Status);
            b.HasIndex(x => x.AppId);
            b.HasIndex(x => x.RentTime);
            b.HasIndex(x => x.ReturnTime);

            b.HasIndex(x => x.IsDeleted);
        });

        builder.Entity<LockerRentLog>(b =>
        {
            b.ToTable(SharedLockerDbProperties.DbTablePrefix + "LockerRentLogs", SharedLockerDbProperties.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Action).HasMaxLength(LockerRentLogConst.MaxRentInfoLogActionLength);
            b.Property(x => x.Description).HasMaxLength(LockerRentLogConst.MaxRentInfoLogDescriptionLength);

            b.HasOne(x => x.LockerRent);
        });

        builder.Entity<LockerRentInfo>(b =>
        {
            b.ToTable(SharedLockerDbProperties.DbTablePrefix + "LockerRentInfos", SharedLockerDbProperties.DbSchema);
            b.ConfigureByConvention();

            b.HasOne(x => x.LockerRent).WithMany(s => s.RentInfos).HasForeignKey(x => x.RentId);
            b.HasOne(x => x.Locker).WithMany(s => s.RentInfos).HasForeignKey(x => x.LockerId);
        });

        builder.Entity<LockerRentApply>(b =>
        {
            b.ToTable(SharedLockerDbProperties.DbTablePrefix + "LockerRentApplies", SharedLockerDbProperties.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name).HasMaxLength(LockerRentApplyConst.MaxNameLength);
            b.Property(x => x.Phone).HasMaxLength(LockerRentApplyConst.MaxPhoneLength);
            b.Property(x => x.PinyinName).HasMaxLength(LockerRentApplyConst.MaxPinyinNameLength);
            b.Property(x => x.FullPinyinName).HasMaxLength(LockerRentApplyConst.MaxFullPinyinNameLength);
            b.Property(x => x.Auditor).HasMaxLength(LockerRentApplyConst.MaxNameLength);
            b.Property(x => x.AuditRemark).HasMaxLength(LockerRentApplyConst.MaxRemarkLength);
            b.Property(x => x.DiscardReason).HasMaxLength(LockerRentApplyConst.MaxDiscardReasonLength);
            b.Property(x => x.CancelReason).HasMaxLength(LockerRentApplyConst.MaxCancelReasonLength);

            b.HasOne(x => x.LockerRent);

            b.HasIndex(x => x.Name);
            b.HasIndex(x => x.PinyinName);
            b.HasIndex(x => x.Phone);
            b.HasIndex(x => x.Status);
            b.HasIndex(x => x.AppId);
            b.HasIndex(x => x.RentTime);
            b.HasIndex(x => x.IsDeleted);
        });
    }
}
