using System;
using System.Threading.Tasks;
using SharedLocker.Domain.SharedLockers;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SharedLocker.EntityFrameworkCore.Domain.SharedLockers
{
    public class LockerRepositoryTests : SharedLockerEntityFrameworkCoreTestBase
    {
        private readonly IRepository<Locker, Guid> _lockerRepository;

        public LockerRepositoryTests()
        {
            _lockerRepository = GetRequiredService<IRepository<Locker, Guid>>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
        */
    }
}
