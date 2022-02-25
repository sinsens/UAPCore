namespace SharedLocker;

public static class SharedLockerDbProperties
{
    public static string DbTablePrefix { get; set; } = "SharedLocker_";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "SharedLocker";
}
