using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Permissions;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    [Authorize]
    public class UserInfoAppService : ReadOnlyAppService<UserInfo, UserInfoDto, Guid, PagedAndSortedResultRequestDto>,
        IUserInfoAppService
    {
        //protected override string GetPolicyName { get; set; } = MiniProgramsPermissions.UserInfo.Default;
        //protected override string GetListPolicyName { get; set; } = MiniProgramsPermissions.UserInfo.Default;

        private readonly IUserInfoRepository _repository;
        private readonly IdentityUserManager _identityUserManager;

        public UserInfoAppService(IUserInfoRepository repository, IdentityUserManager identityUserManager) : base(repository)
        {
            _repository = repository;
            _identityUserManager = identityUserManager;
        }

        [Authorize]
        public async Task<UserInfoDto> UpdateAsync(UserInfoModel input)
        {
            var userInfo = await _repository.FindAsync(x => x.UserId == CurrentUser.GetId());

            userInfo.UpdateInfo(input);

            await _repository.UpdateAsync(userInfo, true);

            return await MapToGetOutputDtoAsync(userInfo);
        }

        [Authorize]
        public async ValueTask UpdateAsync(Guid id, UpdateNameAndPhoneDto input)
        {
            Console.WriteLine("================================");
            Console.WriteLine($"Current-Tenant: {CurrentTenant.Id}");
            Console.WriteLine($"Current-User: {CurrentUser.Id}");
            var user = await _identityUserManager.GetByIdAsync(id);
            if (user != null)
            {
                if (!input.Name.IsNullOrWhiteSpace())
                {
                    user.Name = input.Name;
                }
                if (!input.Phone.IsNullOrWhiteSpace())
                {
                    user.SetPhoneNumber(input.Phone, input.PhoneIsConfirm);
                }
                await _identityUserManager.UpdateAsync(user);
            }
        }
    }
}