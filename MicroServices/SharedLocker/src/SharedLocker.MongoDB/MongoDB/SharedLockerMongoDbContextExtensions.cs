using Volo.Abp;
using Volo.Abp.MongoDB;

namespace SharedLocker.MongoDB;

public static class SharedLockerMongoDbContextExtensions
{
    public static void ConfigureSharedLocker(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
