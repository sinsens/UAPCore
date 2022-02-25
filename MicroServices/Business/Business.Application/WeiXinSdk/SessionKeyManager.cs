using Business.WeiXinSdk.Dto;
using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace Business.WeiXinSdk
{
    public class SessionKeyManager : ISingletonDependency
    {
        private readonly IDistributedCache<Code2SessionResponseDto, string> _sessionKeyStore;
        public SessionKeyManager(IDistributedCache<Code2SessionResponseDto, string> cache)
        {
            _sessionKeyStore = cache;
        }

        public async ValueTask<Code2SessionResponseDto> GetByOpenIdAsync(string openId)
        {
            return await _sessionKeyStore.GetAsync(openId);
        }

        public async ValueTask AddOrUpdateAsync(string openId, Code2SessionResponseDto code2Session)
        {
            await _sessionKeyStore.GetOrAddAsync(openId, 
                () => { 
                    return Task.FromResult(code2Session); 
                }, ()=> new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions { 
                    SlidingExpiration = TimeSpan.FromHours(2) 
                });
        }
    }
}
