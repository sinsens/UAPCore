using Volo.Abp.Reflection;

namespace SharedLocker.Permissions;

public class SharedLockerPermissions
{
    public const string GroupName = "SharedLocker";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(SharedLockerPermissions));
    }

        public class Locker
        {
            public const string Default = GroupName + ".Locker";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class LockerRent
        {
            public const string Default = GroupName + ".LockerRent";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
}
