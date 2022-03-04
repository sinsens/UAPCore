using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using UAP.Shared;

namespace SharedLocker.Samples;

public class SampleAppService : SharedLockerAppService, ISampleAppService
{
    public SampleAppService(ICurrentApp currentApp) : base(currentApp)
    {
    }

    public Task<SampleDto> GetAsync()
    {
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }

    [Authorize]
    public Task<SampleDto> GetAuthorizedAsync()
    {
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }
}
