using SharedLocker.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace SharedLocker.Permissions;

public class SharedLockerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
            var myGroup = context.AddGroup(SharedLockerPermissions.GroupName, L("Permission:SharedLocker"));

            var lockerPermission = myGroup.AddPermission(SharedLockerPermissions.Locker.Default, L("Permission:Locker"));
            lockerPermission.AddChild(SharedLockerPermissions.Locker.Create, L("Permission:Create"));
            lockerPermission.AddChild(SharedLockerPermissions.Locker.Update, L("Permission:Update"));
            lockerPermission.AddChild(SharedLockerPermissions.Locker.Delete, L("Permission:Delete"));

            var lockerRentPermission = myGroup.AddPermission(SharedLockerPermissions.LockerRent.Default, L("Permission:LockerRent"));
            lockerRentPermission.AddChild(SharedLockerPermissions.LockerRent.Create, L("Permission:Create"));
            lockerRentPermission.AddChild(SharedLockerPermissions.LockerRent.Update, L("Permission:Update"));
            lockerRentPermission.AddChild(SharedLockerPermissions.LockerRent.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SharedLockerResource>(name);
    }
}
