using Volo.Abp.Reflection;

namespace SharedLocker.Permissions;

/// <summary>
/// �������Ȩ��
/// </summary>
public class SharedLockerPermissions
{
    public const string GroupName = "SharedLocker";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(SharedLockerPermissions));
    }

    /// <summary>
    /// �����
    /// </summary>
    public class Locker
    {
        public const string Default = GroupName + ".Locker";
        /// <summary>
        /// ����
        /// </summary>
        public const string Update = Default + ".Update";
        /// <summary>
        /// ����
        /// </summary>
        public const string Create = Default + ".Create";
        /// <summary>
        /// ɾ��
        /// </summary>
        public const string Delete = Default + ".Delete";
    }

    /// <summary>
    /// �������
    /// </summary>
    public class LockerRent
    {
        public const string Default = GroupName + ".LockerRent";
        /// <summary>
        /// ����
        /// </summary>
        public const string Create = Default + ".Create";
        /// <summary>
        /// ɾ��
        /// </summary>
        public const string Delete = Default + ".Delete";
        /// <summary>
        /// �黹
        /// </summary>
        public const string Return = Default + ".Return";
        /// <summary>
        /// ����
        /// </summary>
        public const string Discard = Default + ".Discard";
    }

    /// <summary>
    /// �����������
    /// </summary>
    public class RentApply
    {
        public const string Default = GroupName + ".RentApply";
        /// <summary>
        /// ����
        /// </summary>
        public const string Create = Default + ".Create";
        /// <summary>
        /// ���
        /// </summary>
        public const string Audit = Default + ".Audit";
        /// <summary>
        /// ȡ��
        /// </summary>
        public const string Cancel = Default + ".Cancel";
        /// <summary>
        /// ɾ��
        /// </summary>
        public const string Delete = Default + ".Delete";
        /// <summary>
        /// ����
        /// </summary>
        public const string Discard = Default + ".Discard";
    }
}
