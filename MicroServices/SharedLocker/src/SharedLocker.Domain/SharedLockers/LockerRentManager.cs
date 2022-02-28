using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SharedLocker.Domain.SharedLockers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAP.Shared;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;
using SharedLocker.Localization;
using ToolGood.Words.Pinyin;

namespace SharedLocker.SharedLockers
{
    public class LockerRentManager : DomainService
    {
        private readonly ICurrentApp _currentApp;
        private readonly ICurrentUser _currentUser;
        private readonly IStringLocalizer<SharedLockerResource> _stringLocalizer;
        private readonly IRepository<LockerRent, Guid> _repository;
        private readonly IRepository<Locker, Guid> _repositoryLocker;
        private readonly IRepository<LockerRentInfo, Guid> _repositoryLink;

        public LockerRentManager(ICurrentApp currentApp,
            ICurrentUser currentUser,
            IStringLocalizer<SharedLockerResource> stringLocalizer,
            IRepository<LockerRent, Guid> repository,
            IRepository<Locker, Guid> repositoryLocker,
            IRepository<LockerRentInfo, Guid> repositoryLink)
        {
            _currentApp = currentApp;
            _currentUser = currentUser;
            _stringLocalizer = stringLocalizer;
            _repository = repository;
            _repositoryLocker = repositoryLocker;
            _repositoryLink = repositoryLink;
        }

        /// <summary>
        /// 租用
        /// </summary>
        /// <param name="appId">App Id</param>
        /// <param name="name">租用人</param>
        /// <param name="phone">手机号码</param>
        /// <param name="remark">备注信息</param>
        /// <param name="rentTime">租用时间</param>
        /// <param name="lockerIds">储物柜ID</param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async ValueTask<LockerRent> RentAsync(Guid? appId, string name, string phone, string remark, DateTime? rentTime, IEnumerable<Guid> lockerIds)
        {
            appId = appId ?? _currentApp.AppId;

            if(lockerIds.Count() < 1)
            {
                throw new UserFriendlyException(_stringLocalizer["LockerRentLockerZero"]);
            }

            var lockers = await _repositoryLocker.GetListAsync(x => x.Status == Enums.LockerStatus.Idle && x.IsActive && lockerIds.Contains(x.Id));

            if (lockers.Count < lockerIds.Count())
            {
                throw new UserFriendlyException(_stringLocalizer["LockerRentNumberNotEqual"]);
            }

            // 关联储物柜
            var lockerRent = new LockerRent(GuidGenerator.Create(), CurrentTenant.Id, appId, name, phone, remark, rentTime ?? Clock.Now);
            lockerRent.SetLockers(lockerIds);

            // 获取拼音
            var pinyinName = WordsHelper.GetFirstPinyin(name);
            var fullPinyinName = WordsHelper.GetPinyinForName(name, true);
            lockerRent.SetPinyinName(pinyinName, fullPinyinName);
            WordsHelper.ClearCache();

            // 添加日志
            var description = $"租用储物柜：{string.Join(",", lockers.Select(x => x.Number))}";
            lockerRent.AddLog(_currentUser.Name, "租用", description);

            await _repository.InsertAsync(lockerRent);

            // 更新储物柜状态
            foreach (var locker in lockers)
            {
                locker.SetStatus(Enums.LockerStatus.Busy);
            }

            await _repositoryLocker.UpdateManyAsync(lockers);

            return lockerRent;
        }

        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="rentId">出租ID</param>
        /// <param name="returnTime">归还时间</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async ValueTask ReturnAsync(Guid id, DateTime returnTime, string remark)
        {
            var lockerRent = await GetLockerRentAsync(id);

            if ((returnTime - Clock.Now.AddDays(1)).TotalSeconds > 5)
            {
                throw new UserFriendlyException(_stringLocalizer["LockerRentReturnTimeOverError"]);
            }

            if (lockerRent == null)
            {
                throw new UserFriendlyException(_stringLocalizer["ParameterError"]);
            }

            if (lockerRent.Status != Enums.LockerRentStatus.InService)
            {
                throw new UserFriendlyException(_stringLocalizer["LockerRentStatusError"]);
            }

            if (returnTime < lockerRent.RentTime)
            {
                throw new UserFriendlyException(_stringLocalizer["LockerRentReturnTimeLessError"]);
            }

            var lockerIds = lockerRent.RentInfos.Select(x => x.LockerId);
            var lockers = await _repositoryLocker.GetListAsync(x => lockerIds.Contains(x.Id));

            // 添加日志
            var description = $"退租归还储物柜：{string.Join(",", lockers.Select(x => x.Number))}";
            lockerRent.AddLog(_currentUser.Name, "退租", description);

            // 更新状态

            lockerRent.SetReturn(returnTime, remark);
            foreach (var locker in lockers)
            {
                locker.SetStatus(Enums.LockerStatus.Idle);
            }

            await _repositoryLocker.UpdateManyAsync(lockers);
            await _repository.UpdateAsync(lockerRent);
        }

        public async ValueTask DiscardAsync(Guid id)
        {
            var lockerRent = await GetLockerRentAsync(id);

            if (lockerRent.Status != Enums.LockerRentStatus.Discard)
            {
                var lockerIds = lockerRent.RentInfos.Select(x => x.LockerId);
                var lockers = await _repositoryLocker.GetListAsync(x => lockerIds.Contains(x.Id));

                // 添加日志
                var description = "作废出租信息";
                lockerRent.AddLog(_currentUser.Name, "作废", description);

                // 更新状态
                lockerRent.SetDiscard();
                foreach (var locker in lockers)
                {
                    locker.SetStatus(Enums.LockerStatus.Idle);
                }

                await _repositoryLocker.UpdateManyAsync(lockers);
                await _repository.UpdateAsync(lockerRent);
            }
        }

        public async ValueTask DeleteAsync(Guid id)
        {
            var lockerRent = await GetLockerRentAsync(id);

            if (lockerRent.Status != Enums.LockerRentStatus.Discard)
            {
                throw new UserFriendlyException(_stringLocalizer["LockerRentStatusError"]);
            }
            
            var lockerLinkIds = lockerRent.RentInfos.Select(x => x.Id);

            await _repositoryLink.DeleteManyAsync(lockerLinkIds);
            await _repository.DeleteAsync(id);
        }

        protected async ValueTask<LockerRent> GetLockerRentAsync(Guid id)
        {
            var lockerRentQuery = await _repository.GetQueryableAsync();

            var lockerRent = await lockerRentQuery
                .Include(x => x.RentInfos)
                .Include(x => x.Logs)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (lockerRent == null)
            {
                throw new UserFriendlyException(_stringLocalizer["ParameterError"]);
            }

            return lockerRent;
        }
    }
}
