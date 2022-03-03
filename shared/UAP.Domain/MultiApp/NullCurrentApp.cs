using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using UAP.Shared;
using Volo.Abp.DependencyInjection;

namespace UAP.Domain.MultiApp
{
    public class NullCurrentApp : ICurrentApp, IScopedDependency
    {
        public Guid? AppId => GetCurrentApp();

        private Guid? appId;

        private bool hasLoad = false;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public NullCurrentApp(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
                var request = _httpContextAccessor.HttpContext?.Request;
                var idString = request?.Headers[UAPConsts.AppIdKey] ?? request?.Query[UAPConsts.AppIdKey].FirstOrDefault();
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
