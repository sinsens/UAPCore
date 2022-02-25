using Business.Domain.UniApp;
using Business.Shared.Consts;
using Business.UniApp;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;
using Volo.Abp.Users;

namespace Business.Services
{
    public class CurrentAppProvider : IScopedDependency, ICurrentApp
    {
        private Guid? appId;
        private Guid? userId;
        private bool hasLoad = false;
        
        private readonly AppManager appManager;
        private readonly AppUserManager appUserManager;
        private readonly IHttpContextAccessor httpContext;

        public CurrentAppProvider(AppManager appManager, AppUserManager userManager, ICurrentUser currentUser, IHttpContextAccessor contextAccessor)
        {
            userId = currentUser.Id;
            this.appManager = appManager;
            appUserManager = userManager;
            httpContext = contextAccessor;
        }

        public Guid? AppId
        {
            get
            {
                if (!hasLoad)
                {
                    App currentApp = null;
                    if (userId.HasValue)
                    {
                        AsyncHelper.RunSync(async() =>
                        {
                            var appUser = await appUserManager.GetCacheAsync(userId.Value);
                            if (appUser != null)
                            {
                                if(!appUser.IsActive)
                                    throw new UserFriendlyException("当前用户已被禁用");

                                currentApp = await appManager.GetCacheAsync(appUser.AppId.Value);
                            }
                        });
                    }
                    else
                    {
                        var appIdString = httpContext.HttpContext.Request.Headers[AppConsts.APP_ID_HEADER_NAME].FirstOrDefault() ?? httpContext.HttpContext.Request.Query[AppConsts.APP_ID_HEADER_NAME];
                        if (!appIdString.IsNullOrWhiteSpace())
                        {
                            AsyncHelper.RunSync(async () =>
                            {
                                currentApp = await appManager.GetCacheByAppIdAsync(appIdString);
                            });
                        }
                    }

                    if (currentApp != null && !currentApp.IsActive)
                    {
                        throw new UserFriendlyException("当前应用已被禁用");
                    }

                    appId = currentApp?.Id;

                    hasLoad = true;
                }
                return appId;
            }
        }
    }
}
