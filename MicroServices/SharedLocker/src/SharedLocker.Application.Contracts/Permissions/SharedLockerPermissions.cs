using Volo.Abp.Reflection;

namespace SharedLocker.Permissions;

/// <summary>
/// 共享储物柜权限
/// </summary>
public class SharedLockerPermissions
{
    public const string GroupName = "SharedLocker";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(SharedLockerPermissions));
    }

    /// <summary>
    /// 储物柜
    /// </summary>
    public class Locker
    {
        public const string Default = GroupName + ".Locker";
        /// <summary>
        /// 更新
        /// </summary>
        public const string Update = Default + ".Update";
        /// <summary>
        /// 创建
        /// </summary>
        public const string Create = Default + ".Create";
        /// <summary>
        /// 删除
        /// </summary>
        public const string Delete = Default + ".Delete";
    }

    /// <summary>
    /// 出租管理
    /// </summary>
    public class LockerRent
    {
        public const string Default = GroupName + ".LockerRent";
        /// <summary>
        /// 创建
        /// </summary>
        public const string Create = Default + ".Create";
        /// <summary>
        /// 删除
        /// </summary>
        public const string Delete = Default + ".Delete";
        /// <summary>
        /// 归还
        /// </summary>
        public const string Return = Default + ".Return";
        /// <summary>
        /// 作废
        /// </summary>
        public const string Discard = Default + ".Discard";
    }

    /// <summary>
    /// 租用申请管理
    /// </summary>
    public class RentApply
    {
        public const string Default = GroupName + ".RentApply";
        /// <summary>
        /// 创建
        /// </summary>
        public const string Create = Default + ".Create";
        /// <summary>
        /// 审核
        /// </summary>
        public const string Audit = Default + ".Audit";
        /// <summary>
        /// 取消
        /// </summary>
        public const string Cancel = Default + ".Cancel";
        /// <summary>
        /// 删除
        /// </summary>
        public const string Delete = Default + ".Delete";
        /// <summary>
        /// 作废
        /// </summary>
        public const string Discard = Default + ".Discard";
    }
}
