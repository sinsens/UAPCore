using Business.Core.Options;
using Business.Domain.UniApp;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.MultiTenancy;

namespace Business.UniApp
{
    public class AppManager : DomainService
    {
        protected readonly IDataFilter DataFilter;
        protected readonly IRepository<App, Guid> Repository;
        protected readonly IDistributedCache<App, Guid> AppCache;
        protected readonly AppIdAndSecretOptions IdAndSecretOptions;
        protected readonly IDistributedCache<App, string> AppIdCache;


        public AppManager(IOptions<AppIdAndSecretOptions> options, IRepository<App, Guid> repository, IDataFilter dataFilter, IDistributedCache<App, Guid> appCache, IDistributedCache<App, string> appIdCache)
        {
            Repository = repository;
            DataFilter = dataFilter;
            AppCache = appCache;
            AppIdCache = appIdCache;
            IdAndSecretOptions = options.Value;
        }

        /// <summary>
        /// 创建 App
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="appSecret">appSecret</param>
        /// <param name="appName">App 名称</param>
        /// <param name="appType">App 类型</param>
        /// <param name="isActive">是否激活：是</param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async ValueTask<App> CreateAsync(string appId, string appSecret, string appName, AppType? appType, bool isActive = true)
        {
            var app = new App(GuidGenerator.Create(), CurrentTenant.Id, appName, appId, appSecret, appType, isActive);

            await Repository.InsertAsync(app);

            return app;
        }

        public async ValueTask<App> GetCacheAsync([NotNull] Guid? id)
        {
            using (DataFilter.Disable<IMultiTenant>())
            {
                if (!id.HasValue) return null;
                return await AppCache.GetOrAddAsync(id.Value, async () =>
                {
                    return await Repository.FirstOrDefaultAsync(x => x.Id == id);
                }, () =>
                {
                    return new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions { AbsoluteExpiration = Clock.Now.AddHours(2) };
                });
            }
        }

        public async ValueTask<App> GetCacheByAppIdAsync([NotNull]string appId)
        {
            using (DataFilter.Disable<IMultiTenant>())
            {
                return await AppIdCache.GetOrAddAsync(appId, async () =>
                {
                    return await Repository.FirstOrDefaultAsync(x => x.AppId == appId);
                }, () =>
                {
                    return new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions { AbsoluteExpiration = Clock.Now.AddHours(2) };
                });
            }
        }

        public async ValueTask RemoveCacheAsync(Guid? id, string appId = null)
        {
            if (id.HasValue)
            {
                await AppCache.RemoveAsync(id.Value);
            }
            using (DataFilter.Disable<IMultiTenant>())
            {
                if (id.HasValue && appId.IsNullOrWhiteSpace())
                {

                    var app = await Repository.FirstOrDefaultAsync(x => x.Id == id);

                    if (app?.AppId.IsNullOrWhiteSpace() != false)
                    {
                        await AppIdCache.RemoveAsync(app.AppId);
                    }
                }
                else if (!appId.IsNullOrWhiteSpace())
                {
                    await AppIdCache.RemoveAsync(appId);
                }
            }
        }


        #region AppId 和 AppSecret 生成
        public class AppIdAndSecret
        {
            public string AppId;
            public string AppSecret;
        }

        const string keyDatas = "abcdefghijklmnopqrstuvwxyz0123456789";
        public AppIdAndSecret GenerateAppIdAndAppSecret(int idLength, int secretLength)
        {
            var result = new AppIdAndSecret();
            var rand = new Random();
            var sb = new StringBuilder(32);

            for (int i = 0; i < idLength; i++)
            {
                sb.Append(keyDatas[rand.Next(0, keyDatas.Length)]);
            }
            result.AppId = sb.ToString();
            sb.Clear();

            for (int i = 0; i < secretLength; i++)
            {
                sb.Append(keyDatas[rand.Next(0, keyDatas.Length)]);
            }
            result.AppSecret = sb.ToString();
            sb.Clear();

            return result;
        }
        #endregion
    }
}
