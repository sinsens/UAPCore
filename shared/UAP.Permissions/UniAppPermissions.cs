namespace UAP.Permissions
{
    public static class UniAppPermissions
    {
        public const string GroupName = "UniApp";

        public static class Pages
        {
            public const string Default = GroupName + ".Pages";
        }

        public static class Functions
        {
            public const string Default = GroupName + ".Actions";
        }

        public static class UserLookup
        {
            public const string Default = GroupName + ".UserLookup";
        }
    }
}
