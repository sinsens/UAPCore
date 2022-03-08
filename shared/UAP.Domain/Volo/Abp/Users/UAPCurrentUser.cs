using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Principal;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace UAP.Domain.Volo.Abp.Users
{
    /// <summary>
    /// Volo.Abp.Security\Volo\Abp\Users\CurrentUser.cs
    /// </summary>
    public class UAPCurrentUser : CurrentUser, ICurrentUser, ITransientDependency
    {
        public override Guid? Id
        {
            get
            {
                var id = _principalAccessor.Principal?.FindUserId();

                if (id == null)
                {
                    var request = _httpContextAccessor.HttpContext?.Request;
                    var idString = request?.Headers[UAPConsts.UserIdKey] ?? request?.Query[UAPConsts.UserIdKey].FirstOrDefault();
                    if (Guid.TryParse(idString, out Guid userid))
                    {
                        id = userid;
                    }
                }
                return id;
            }
        }

        public override Guid? TenantId
        {
            get
            {
                var id = base.TenantId;
                if(id == null)
                {
                    var request = _httpContextAccessor.HttpContext?.Request;
                    var idString = request?.Headers[UAPConsts.TenantIdKey] ?? request?.Query[UAPConsts.TenantIdKey].FirstOrDefault();
                    if (Guid.TryParse(idString, out Guid tenantid))
                    {
                        id = tenantid;
                    }
                }
                return id;
            }
        }

        private readonly ICurrentPrincipalAccessor _principalAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UAPCurrentUser(ICurrentPrincipalAccessor principalAccessor, IHttpContextAccessor httpContextAccessor)
            : base(principalAccessor)
        {
            _principalAccessor = principalAccessor;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
