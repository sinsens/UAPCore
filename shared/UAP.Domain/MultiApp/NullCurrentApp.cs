using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using UAP.Shared;
using Volo.Abp.DependencyInjection;

namespace UAP.Domain.MultiApp
{
    public class NullCurrentApp : ICurrentApp, IScopedDependency
    {
        public Guid? AppId { get => GetCurrentApp(); }

        private Guid? appId;

        private bool hasLoad = false;

        private readonly AbpLazyServiceProvider serviceProvider;
        public NullCurrentApp(AbpLazyServiceProvider abpLazyServiceProvider)
        {
            serviceProvider = abpLazyServiceProvider;
        }

        private Guid? GetCurrentApp()
        {
            if (hasLoad)
            {
                return appId;
            }
            lock (this)
            {
                Guid id = default;
                var httpContext = serviceProvider.LazyGetRequiredService<IHttpContextAccessor>();
                var idString = httpContext.HttpContext?.Request.Query[CurrentAppConsts.AppIdKey].FirstOrDefault()
                    ?? httpContext.HttpContext?.Request.Headers[CurrentAppConsts.AppIdKey];
                if (Guid.TryParse(idString, out id))
                {
                    appId = id;
                }
                hasLoad = true;
            }
            return appId;
        }
    }
}
