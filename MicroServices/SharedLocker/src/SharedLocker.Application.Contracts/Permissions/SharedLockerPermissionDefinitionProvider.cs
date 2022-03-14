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
        lockerRentPermission.AddChild(SharedLockerPermissions.LockerRent.Delete, L("Permission:Delete"));
        lockerRentPermission.AddChild(SharedLockerPermissions.LockerRent.Discard, L("Permission:LockerRent:Discard"));
        lockerRentPermission.AddChild(SharedLockerPermissions.LockerRent.Return, L("Permission:LockerRent:Return"));

        var rentApplyPermission = myGroup.AddPermission(SharedLockerPermissions.RentApply.Default, L("Permission:RentApply"));
        rentApplyPermission.AddChild(SharedLockerPermissions.RentApply.Create, L("Permission:RentApply:Create"));
        rentApplyPermission.AddChild(SharedLockerPermissions.RentApply.Audit, L("Permission:RentApply:Audit"));
        rentApplyPermission.AddChild(SharedLockerPermissions.RentApply.Cancel, L("Permission:RentApply:Cancel"));
        rentApplyPermission.AddChild(SharedLockerPermissions.RentApply.Delete, L("Permission:RentApply:Delete"));
        rentApplyPermission.AddChild(SharedLockerPermissions.RentApply.Discard, L("Permission:RentApply:Discard"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SharedLockerResource>(name);
    }
}
