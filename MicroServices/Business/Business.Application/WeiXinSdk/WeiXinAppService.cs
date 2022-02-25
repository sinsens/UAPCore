using Business.AppUserManagement;
using Business.UniApp;
using Business.WeiXinSdk.Dto;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace Business.WeiXinSdk
{
    internal class WeiXinAppService : IWeiXinAppService, IScopedDependency
    {
        private readonly AppManager _appManager;
        private readonly ICurrentApp _currentApp;
        private readonly IObjectMapper _objectMapper;
        private readonly SessionKeyManager _sessionKeyManager;
        private readonly IAppUserAppService _appUserAppService;
        private readonly IAuthenticationService _authenticationService;
        

        public WeiXinAppService(
            ICurrentApp currentApp, 
            AppManager appManager,
            IObjectMapper objectMapper,
            SessionKeyManager sessionKeyManager,
            IAppUserAppService appUserAppService,
            IAuthenticationService authenticationService
            )
        {
            _appManager = appManager;
            _currentApp = currentApp;
            _objectMapper = objectMapper;
            _sessionKeyManager = sessionKeyManager;
            _appUserAppService = appUserAppService;
            _authenticationService = authenticationService;
        }

        public ValueTask<Code2SessionResponseDto> Code2SessionAsync(WeiXinLoginRequestDto input)
        {
            throw new NotImplementedException();
        }

        public ValueTask<string> LoginAsync(WeiXinUserProfileDto input)
        {
            throw new NotImplementedException();
        }
    }
}
