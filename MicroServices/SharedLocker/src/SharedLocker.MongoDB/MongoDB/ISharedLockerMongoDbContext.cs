using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace SharedLocker.MongoDB;

[ConnectionStringName(SharedLockerDbProperties.ConnectionStringName)]
public interface ISharedLockerMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
