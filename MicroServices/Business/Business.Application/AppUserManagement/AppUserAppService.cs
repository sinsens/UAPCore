using Business.AppUserManagement.Dto;
using Business.Domain.UniApp;
using Business.Shared.Consts;
using Business.UniApp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Business.AppUserManagement
{
    public class AppUserAppService : CrudAppService<AppUser, AppUserDto, Guid, GetAppUserInputDto, CreateOrUpdateAppUserDto>, IAppUserAppService
    {
        protected readonly AppUserManager AppUserManager;
        protected readonly IIdentityUserAppService IdentityUserApp;
        public AppUserAppService(IRepository<AppUser, Guid> repository, AppUserManager appUserManager, IIdentityUserAppService identityUserApp) : base(repository)
        {
            AppUserManager = appUserManager;
            IdentityUserApp = identityUserApp;
        }

        public override async Task<AppUserDto> CreateAsync(CreateOrUpdateAppUserDto input)
        {
            var appUser = await AppUserManager.CreateAsync(input.OpenId, input.Name, input.Phone, input.City, input.Address, input.Birth, input.Gender);

            var identityUser = await IdentityUserApp.FindByUsernameAsync(input.OpenId);
            if(identityUser == null)
            {
                await IdentityUserApp.CreateAsync(
                new IdentityUserCreateDto
                {
                    UserName = input.OpenId,
                    Name = input.Name,
                    IsActive = true,
                    PhoneNumber = input.Phone,
                    RoleNames = new string[] { AppUserRoles.DefaultAppRoleName }
                });
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return await MapToGetOutputDtoAsync(appUser);
        }

        public async ValueTask<AppUserDto> CreateAsync(CreateWeiXinAppUserDto input)
        {
            var appUser = await AppUserManager.CreateAsync(input.OpenId, input.Name, input.Phone, input.City, input.Address, input.Birth, input.Gender);

            appUser.ExtraProperties.Add("UserInfoData", input.UserInfoData);

            var identityUser = await IdentityUserApp.FindByUsernameAsync(input.OpenId);
            if (identityUser == null)
            {
                await IdentityUserApp.CreateAsync(
                new IdentityUserCreateDto
                {
                    UserName = input.OpenId,
                    Name = input.Name,
                    IsActive = true,
                    PhoneNumber = input.Phone,
                    RoleNames = new string[] { AppUserRoles.DefaultAppRoleName }
                });
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return await MapToGetOutputDtoAsync(appUser);
        }

        public override async Task<AppUserDto> GetAsync(Guid id)
        {
            var appUser = await AppUserManager.GetCacheAsync(id);

            return await MapToGetOutputDtoAsync(appUser);
        }

        public override async Task<AppUserDto> UpdateAsync(Guid id, CreateOrUpdateAppUserDto input)
        {
            await AppUserManager.RemoveCacheAsync(id);
            return await base.UpdateAsync(id, input);
        }

        public async ValueTask UpdatePhoneAsync(Guid id, UpdatePhoneRequestDto input)
        {
            var appUser = await AppUserManager.GetAsync(id);

            if(appUser != null)
            {
                appUser.SetPhone(input.Phone);

                await AppUserManager.RemoveCacheAsync(id);
            }
        }

        protected override async Task<IQueryable<AppUser>> CreateFilteredQueryAsync(GetAppUserInputDto input)
        {
            var query = await Repository.GetQueryableAsync();

            var likeFilter = $"%{input.Filter}%";

            query = query.WhereIf(input.AppId.HasValue, x => x.AppId == input.AppId)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x =>
                    EF.Functions.Like(x.Name, likeFilter) || EF.Functions.Like(x.Phone, likeFilter) ||
                    EF.Functions.Like(x.City, likeFilter))
                .WhereIf(input.Gender.HasValue, x => x.Gender == input.Gender);

            return query;
        }
    }
}
