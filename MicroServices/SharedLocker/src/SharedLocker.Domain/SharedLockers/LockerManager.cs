using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SharedLocker.Domain.SharedLockers;
using SharedLocker.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using UAP.Shared;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using SharedLocker.Localization;

namespace SharedLocker.SharedLockers
{
    public class LockerManager : DomainService
    {
        private readonly ICurrentApp _currentApp;
        private readonly IRepository<Locker, Guid> _repository;
        private readonly IStringLocalizer<SharedLockerResource> _stringLocalizer;
        public LockerManager(ICurrentApp currentApp, IRepository<Locker, Guid> repository, IStringLocalizer<SharedLockerResource> stringLocalizer)
        {
            _currentApp = currentApp;
            _repository = repository;
            _stringLocalizer = stringLocalizer;
        }

        public async ValueTask<Locker> CreateAsync(Guid? appId, string name, int number, LockerStatus? status, bool isActive = true)
        {
            appId = appId ?? _currentApp.AppId;
            var checkIsExist = await _repository.AnyAsync(x => x.AppId == appId && x.Number == number);
            if (checkIsExist)
            {
                throw new UserFriendlyException(_stringLocalizer["LockerDuplicatedNumber"]);
            }
            var locker = new Locker(GuidGenerator.Create(), CurrentTenant.Id, appId, name, number, isActive);

            if (status.HasValue)
            {
                locker.SetStatus(status.Value);
            }

            await _repository.InsertAsync(locker);

            return locker;
        }

        public async ValueTask<Locker> UpdateAsync(Guid id, string name, int number, LockerStatus? status, bool isActive = true)
        {
            var locker = await _repository.FirstOrDefaultAsync(x => x.Id == id);
            if(locker == null)
            {
                throw new UserFriendlyException(_stringLocalizer["ParameterError"]);
            }

            if (locker.Status == Enums.LockerStatus.Busy)
            {
                throw new UserFriendlyException(_stringLocalizer["LockerIsBusy"]);
            }

            var checkIsExist = await _repository.AnyAsync(x => x.AppId != locker.AppId && x.Number == number);
            if (checkIsExist)
            {
                throw new UserFriendlyException(_stringLocalizer["LockerDuplicatedNumber"]);
            }
            
            if(status.HasValue)
            {
                locker.SetStatus(status.Value);
            }

            locker.SetName(name);
            locker.SetNumber(number);
            locker.SetIsActive(isActive);

            await _repository.UpdateAsync(locker);

            return locker;
        }

        public async ValueTask DeleteAsync(Guid id)
        {
            var query = await _repository.GetQueryableAsync();

            var cannotbeDeleted = await query.Include(x => x.RentInfos).AnyAsync(x => x.Id == id && x.RentInfos.Any());

            if (cannotbeDeleted)
            {
                throw new UserFriendlyException(_stringLocalizer["LockerCannotDelete"]);
            }

            await _repository.DeleteAsync(x=>x.Id == id);
        }
    }
}
