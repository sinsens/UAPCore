using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace SharedLocker.Domain.SharedLockers
{
    public class LockerAppServiceTests : SharedLockerApplicationTestBase
    {
        private readonly ILockerAppService _lockerAppService;

        public LockerAppServiceTests()
        {
            _lockerAppService = GetRequiredService<ILockerAppService>();
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
