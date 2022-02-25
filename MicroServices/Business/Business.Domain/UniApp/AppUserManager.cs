using Business.Domain.UniApp;
using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.MultiTenancy;

namespace Business.UniApp
{
    public class AppUserManager : DomainService
    {
        protected readonly IRepository<AppUser, Guid> Repository;
        protected readonly IDistributedCache<AppUser, Guid> Cache;
        protected readonly IDataFilter DataFilter;
        public AppUserManager(IRepository<AppUser, Guid> repository, IDataFilter dataFilter)
        {
            Repository = repository;
            DataFilter = dataFilter;
        }

        /// <summary>
        /// 创建 AppUser
        /// </summary>
        /// <param name="openId">openId</param>
        /// <param name="name">姓名</param>
        /// <param name="phone">联系电话</param>
        /// <param name="city">所在城市</param>
        /// <param name="address">地址</param>
        /// <param name="birth">生日</param>
        /// <param name="gender">性别</param>
        /// <returns></returns>
        public async ValueTask<AppUser> CreateAsync(string openId, string name, string phone, string city, string address, DateTime? birth, GenderEnum? gender)
        {
            var currentApp = LazyServiceProvider.LazyGetService<ICurrentApp>();

            var appUser = await Repository.FirstOrDefaultAsync(x => x.OpenId == openId && x.AppId == currentApp.AppId);

            if(appUser == null)
            {
                appUser = new AppUser(GuidGenerator.Create(), CurrentTenant.Id, currentApp?.AppId, openId, name, phone);

                appUser.SetAdress(address);
                appUser.SetBirth(birth);
                appUser.SetGender(gender);
                appUser.SetCity(city);

                await Repository.InsertAsync(appUser);
            }
            else
            {
                appUser.SetAdress(address);
                appUser.SetBirth(birth);
                appUser.SetGender(gender);
                appUser.SetCity(city);

                await Repository.UpdateAsync(appUser);
            }

            return appUser;
        }

        /// <summary>
        /// 创建 AppUser
        /// </summary>
        /// <param name="openId">openId</param>
        /// <returns></returns>
        public async ValueTask<AppUser> CreateAsync(string openId)
        {
            var currentApp = LazyServiceProvider.LazyGetService<ICurrentApp>();

            var appUser = await Repository.FirstOrDefaultAsync(x => x.OpenId == openId && x.AppId == currentApp.AppId);

            if (appUser == null)
            {
                appUser = new AppUser(GuidGenerator.Create(), CurrentTenant.Id, currentApp?.AppId, openId);
                await Repository.InsertAsync(appUser);
            }

            return appUser;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<AppUser> GetAsync(Guid id)
        {
            return await Repository.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// 从缓存获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<AppUser> GetCacheAsync(Guid id)
        {
            return await Cache.GetOrAddAsync(id, async () =>
            {
                return await Repository.FirstOrDefaultAsync(x => x.Id == id);
            }, () =>
            {
                return new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions { AbsoluteExpiration = Clock.Now.AddHours(2) };
            });
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask RemoveCacheAsync(Guid id)
        {
            await Cache.RemoveAsync(id);
        }
    }
}
