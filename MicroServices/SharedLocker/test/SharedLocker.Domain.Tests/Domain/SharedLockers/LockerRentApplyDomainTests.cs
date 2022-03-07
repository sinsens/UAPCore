using System;
using System.Threading.Tasks;
using SharedLocker.SharedLockers;
using Shouldly;
using UAP.Shared.Helper;
using Xunit;

namespace SharedLocker.Domain.SharedLockers
{
    public class LockerRentApplyDomainTests : SharedLockerDomainTestBase
    {
        public LockerRentApplyDomainTests()
        {
        }

        
        [Fact]
        public async Task TestApply()
        {
            for (int i = 0; i < 10; i++)
            {
                var apply = new LockerRentApply(GuidHelper.GetSequentialGuid(), GuidHelper.GetSequentialGuid(), GuidHelper.GetSequentialGuid(), GuidHelper.GetSequentialGuid(), "ttt", "18278479807");
                apply.SetRentTime(DateTime.Now);
            }
            // Arrange

            // Assert

            // Assert
        }
    }
}
