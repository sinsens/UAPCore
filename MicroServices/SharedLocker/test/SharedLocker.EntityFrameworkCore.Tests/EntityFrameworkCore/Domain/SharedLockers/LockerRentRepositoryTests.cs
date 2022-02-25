using System;
using System.Threading.Tasks;
using SharedLocker.Domain.SharedLockers;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SharedLocker.EntityFrameworkCore.Domain.SharedLockers
{
    public class LockerRentRepositoryTests : SharedLockerEntityFrameworkCoreTestBase
    {
        private readonly IRepository<LockerRent, Guid> _lockerRentRepository;

        public LockerRentRepositoryTests()
        {
            _lockerRentRepository = GetRequiredService<IRepository<LockerRent, Guid>>();
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
