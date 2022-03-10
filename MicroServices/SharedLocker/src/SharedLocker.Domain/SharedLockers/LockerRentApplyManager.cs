using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SharedLocker.Domain.SharedLockers;
using SharedLocker.Enums;
using SharedLocker.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolGood.Words.Pinyin;
using UAP.Shared;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace SharedLocker.SharedLockers
{
    public class LockerRentApplyManager : DomainService
    {
        private readonly ICurrentApp _currentApp;
        private readonly ICurrentUser _currentUser;
        private readonly IStringLocalizer<SharedLockerResource> _stringLocalizer;
        private readonly IRepository<LockerRent, Guid> _repositoryRent;
        private readonly IRepository<LockerRentApply, Guid> _repositoryApply;
        private readonly LockerRentManager _rentManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public LockerRentApplyManager(ICurrentApp currentApp,
            ICurrentUser currentUser,
            IStringLocalizer<SharedLockerResource> stringLocalizer,
            IRepository<LockerRent, Guid> repositoryrent,
            IRepository<LockerRentApply, Guid> repositoryapply,
			LockerRentManager rentManager,
			IUnitOfWorkManager unitOfWorkManager)
        {
            _currentApp = currentApp;
            _currentUser = currentUser;
            _stringLocalizer = stringLocalizer;
            _repositoryRent = repositoryrent;
            _repositoryApply = repositoryapply;
			_rentManager = rentManager;
			_unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 是否可申请
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async ValueTask CheckCanApplyAsync(Guid userid)
        {
            var flag = await _repositoryApply
                .AnyAsync(x => x.Status == Enums.LockerRentApplyStatus.PendingAudit && x.UserId == userid);
            if (flag)
            {
                throw new UserFriendlyException(_stringLocalizer["HasPendingAuditApply"]);
            }
            var query = await _repositoryApply.GetQueryableAsync();
            flag = await query.Include(x => x.LockerRent).AnyAsync(x => x.UserId == userid && x.LockerRent.Status == Enums.LockerRentStatus.InService);
            if (flag)
            {
                throw new UserFriendlyException(_stringLocalizer["HasInServiceApply"]);
            }
        }

        /// <summary>
        /// 租用申请
        /// </summary>
        /// <param name="name">租用人</param>
        /// <param name="phone">手机号码</param>
        /// <param name="remark">备注信息</param>
        /// <param name="count">租用数量</param>
        /// <param name="rentTime">租用时间</param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async ValueTask<LockerRentApply> RentApplyAsync(string name, string phone, string remark, int count, DateTime rentTime)
        {
            var appId = _currentApp.AppId;

            if (count < 1)
            {
                throw new UserFriendlyException(_stringLocalizer["LockerRentLockerZero"]);
            }

            var userid = Check.NotNull(_currentUser.Id.Value, nameof(_currentUser.Id));

            await CheckCanApplyAsync(userid);

            var rentApply = new LockerRentApply(GuidGenerator.Create(), userid, CurrentTenant.Id, appId, name, phone);
			rentApply.SetApplyCount(count);
            rentApply.SetRentTime(rentTime);

            // 获取拼音
            var pinyinName = WordsHelper.GetFirstPinyin(name);
            var fullPinyinName = WordsHelper.GetPinyinForName(name, true);
            rentApply.SetPinyinName(pinyinName, fullPinyinName);
            WordsHelper.ClearCache();

            await _repositoryApply.InsertAsync(rentApply);

            return rentApply;
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reason">作废原因</param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async ValueTask DiscardAsync(Guid id, string reason)
        {
            var rentApply = await GetRentApplyAsync(id);

            if (rentApply.Status != Enums.LockerRentApplyStatus.Discard && rentApply.Status != Enums.LockerRentApplyStatus.Canceled)
            {
                if(string.IsNullOrWhiteSpace(reason))
                {
                    throw new UserFriendlyException(_stringLocalizer["DiscardReasonCannotBeEmpty"]);
                }

                rentApply.SetDiscard(Clock.Now, reason);

                if (rentApply.LockerRentId.HasValue)
                {
                    await _rentManager.DiscardAsync(rentApply.LockerRentId.Value);
                }
                await _repositoryApply.UpdateAsync(rentApply);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reason">取消原因</param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async ValueTask CancelAsync(Guid id, string reason)
        {
            var rentApply = await GetRentApplyAsync(id);

            if(rentApply.Status != LockerRentApplyStatus.PendingAudit)
            {
                throw new UserFriendlyException(_stringLocalizer["ApplyHasBeenAudited"]);
            }

            if (rentApply.Status != Enums.LockerRentApplyStatus.Discard && rentApply.Status != Enums.LockerRentApplyStatus.Canceled)
            {
                rentApply.SetCancel(Clock.Now, reason);
                /*
                if (rentApply.LockerRentId.HasValue)
                {
                    await _rentManager.DiscardAsync(rentApply.LockerRentId.Value);
                }*/
                await _repositoryApply.UpdateAsync(rentApply);
            }
        }

        /// <summary>
        /// 审核申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accepted">是否通过</param>
        /// <param name="remark">备注</param>
        /// <param name="lockerIds">分配的储物柜</param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async ValueTask AuditAsync(Guid id, bool accepted, string remark, IEnumerable<Guid> lockerIds)
        {
            var rentApply = await GetRentApplyAsync(id);

            if(rentApply.Status == Enums.LockerRentApplyStatus.Discard)
            {
                throw new UserFriendlyException(_stringLocalizer["HasDiscardByApplier"]);
            }

            if(rentApply.Status != Enums.LockerRentApplyStatus.PendingAudit)
            {
                return;
            }

            if(!accepted && remark.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException(_stringLocalizer["RemarkCannotBeEmpty"]);
            }

            var status = accepted ? LockerRentApplyStatus.Accepted : LockerRentApplyStatus.Rejected;
            rentApply.SetAudit(status, _currentUser.Name ?? _currentUser.UserName, remark, Clock.Now);

            if (accepted)
            {
                var rent = await _rentManager.RentAsync(rentApply.AppId, rentApply.Name, rentApply.Phone, "申请通过生成", rentApply.RentTime, lockerIds);
                rent.SetPinyinName(rentApply.PinyinName, rentApply.FullPinyinName);
                await _unitOfWorkManager.Current.CompleteAsync();

                rentApply.SetRentId(rent.Id);
            }

            await _repositoryApply.UpdateAsync(rentApply);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async ValueTask DeleteAsync(Guid id)
        {
            var lockerRent = await GetRentApplyAsync(id);

            if (lockerRent.Status != Enums.LockerRentApplyStatus.Canceled && lockerRent.Status != Enums.LockerRentApplyStatus.Discard)
            {
                throw new UserFriendlyException(_stringLocalizer["LockerRentStatusError"]);
            }

            await _repositoryApply.DeleteAsync(id);
        }

        protected async ValueTask<LockerRentApply> GetRentApplyAsync(Guid id)
        {
            var query = await _repositoryApply.GetQueryableAsync();

            var rentApply = await query.FirstOrDefaultAsync(x => x.Id == id);

            if (rentApply == null)
            {
                throw new UserFriendlyException(_stringLocalizer["ParameterError"]);
            }

            return rentApply;
        }
    }
}
