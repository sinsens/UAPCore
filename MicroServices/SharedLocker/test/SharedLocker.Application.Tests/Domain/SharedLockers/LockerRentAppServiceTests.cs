using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace SharedLocker.Domain.SharedLockers
{
    public class LockerRentAppServiceTests : SharedLockerApplicationTestBase
    {
        private readonly ILockerRentAppService _lockerRentAppService;

        public LockerRentAppServiceTests()
        {
            _lockerRentAppService = GetRequiredService<ILockerRentAppService>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act

            // Assert
        }
        */
    }
}
