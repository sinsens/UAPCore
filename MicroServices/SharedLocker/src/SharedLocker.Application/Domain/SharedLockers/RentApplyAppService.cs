using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SharedLocker.Domain.SharedLockers.Dtos;
using SharedLocker.Permissions;
using SharedLocker.SharedLockers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAP.Shared;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace SharedLocker.Domain.SharedLockers
{
    [Authorize]
    public class RentApplyAppService : SharedLockerAppService, IRentApplyAppService
    {
        private readonly LockerRentApplyManager _applyManager;
        private readonly IRepository<LockerRentApply, Guid> _repository;
        public RentApplyAppService(ICurrentApp currentApp, LockerRentApplyManager applyManager, IRepository<LockerRentApply, Guid> repository) : base(currentApp)
        {
            _applyManager = applyManager;
            _repository = repository;
        }

        [Authorize(SharedLockerPermissions.RentApply.Discard)]
        public async ValueTask DiscardAsync(Guid id, DiscardApplyDto input)
        {
            await _applyManager.DiscardAsync(id, input.Reason);
        }

        [Authorize(SharedLockerPermissions.RentApply.Create)]
        public async ValueTask<LockerRentApplyDto> ApplyAsync(CreateLockerRentApplyDto input)
        {
            var rentApply = await _applyManager.RentApplyAsync(input.Name, input.Phone, input.Remark, input.ApplyCount, input.RentTime);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<LockerRentApply, LockerRentApplyDto>(rentApply);
        }

        [Authorize(SharedLockerPermissions.RentApply.Audit)]
        public async ValueTask AuditAsync(Guid id, AuditRentApplyDto input)
        {
            await _applyManager.AuditAsync(id, input.Result, input.Remark, input.LockerIds);
        }

        [Authorize(SharedLockerPermissions.RentApply.Cancel)]
        public async ValueTask CancelAsync(Guid id, CancelApplyDto input)
        {
            await _applyManager.CancelAsync(id, input.Reason);
        }

        public async ValueTask<LockerRentApplyDto> GetAsync(Guid id)
        {
            var query = await _repository.GetQueryableAsync();

            var entity = await query.Include(x => x.LockerRent).ThenInclude(rent => rent.RentInfos).ThenInclude(x => x.Locker).FirstOrDefaultAsync(x => x.Id == id);

            return ObjectMapper.Map<LockerRentApply, LockerRentApplyDto>(entity);
        }

        [Authorize(SharedLockerPermissions.RentApply.Default)]
        public async ValueTask<LockerRentApplyDto> GetLastAsync()
        {
            var query = await _repository.GetQueryableAsync();

            var entity = await query.Include(x => x.LockerRent)
                .Where(x => x.UserId == CurrentUser.Id)
                .FirstOrDefaultAsync(x=> x.Status == Enums.LockerRentApplyStatus.PendingAudit || x.LockerRent.Status == Enums.LockerRentStatus.InService);

            return ObjectMapper.Map<LockerRentApply, LockerRentApplyDto>(entity);
        }

        [Authorize(SharedLockerPermissions.RentApply.Default)]
        public async ValueTask<PagedResultDto<LockerRentApplyDto>> GetListAsync(PagedAndSortedRentApplyRequestDto input)
        {
            var likeFilter = $"%{input.Filter}%";

            var query = await _repository.GetQueryableAsync();

            query = query
                .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                .WhereIf(input.StartTime.HasValue, x => x.RentTime >= input.StartTime)
                .WhereIf(input.EndTime.HasValue, x => x.RentTime <= input.EndTime)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x =>
                    EF.Functions.Like(x.Name, likeFilter) || EF.Functions.Like(x.PinyinName, likeFilter) ||
                    EF.Functions.Like(x.FullPinyinName, likeFilter) || EF.Functions.Like(x.Phone, likeFilter)
                );

            var count = await query.CountAsync();
            var list = await query.OrderByDescending(x => x.Id).PageBy(input).ToListAsync();

            return new PagedResultDto<LockerRentApplyDto>
            {
                TotalCount = count,
                Items = ObjectMapper.Map<IReadOnlyList<LockerRentApply>, IReadOnlyList<LockerRentApplyDto>>(list)
            };
        }

        [Authorize(SharedLockerPermissions.RentApply.Create)]
        public async ValueTask<PagedResultDto<LockerRentApplyDto>> GetMyListAsync(PagedAndSortedRentApplyCustomerRequestDto input)
        {

            var likeFilter = $"%{input.Filter}%";

            var query = await _repository.GetQueryableAsync();

            query = query
                .Where(x => x.UserId == CurrentUser.Id)
                .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                .WhereIf(input.StartTime.HasValue, x => x.RentTime >= input.StartTime)
                .WhereIf(input.EndTime.HasValue, x => x.RentTime <= input.EndTime)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x =>
                    EF.Functions.Like(x.Name, likeFilter) || EF.Functions.Like(x.PinyinName, likeFilter) ||
                    EF.Functions.Like(x.FullPinyinName, likeFilter) || EF.Functions.Like(x.Phone, likeFilter)
                );

            var count = await query.CountAsync();
            var list = await query.OrderByDescending(x => x.Id).PageBy(input).ToListAsync();

            return new PagedResultDto<LockerRentApplyDto>
            {
                TotalCount = count,
                Items = ObjectMapper.Map<IReadOnlyList<LockerRentApply>, IReadOnlyList<LockerRentApplyDto>>(list)
            };
        }

        [Authorize(SharedLockerPermissions.RentApply.Create)]
        public async ValueTask<PagedResultDto<LockerRentApplyDto>> GetProcessListAsync(PagedAndSortedRentApplyCustomerRequestDto input)
        {
            var likeFilter = $"%{input.Filter}%";

            var query = await _repository.GetQueryableAsync();

            query = query.Include(x => x.LockerRent)
                .Where(x => x.UserId == CurrentUser.Id)
                .Where(x => x.LockerRent.Status == Enums.LockerRentStatus.InService)
                .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                .WhereIf(input.StartTime.HasValue, x => x.RentTime >= input.StartTime)
                .WhereIf(input.EndTime.HasValue, x => x.RentTime <= input.EndTime)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x =>
                    EF.Functions.Like(x.Name, likeFilter) || EF.Functions.Like(x.PinyinName, likeFilter) ||
                    EF.Functions.Like(x.FullPinyinName, likeFilter) || EF.Functions.Like(x.Phone, likeFilter)
                );

            var count = await query.CountAsync();
            var list = await query.OrderByDescending(x => x.Id).PageBy(input).ToListAsync();

            return new PagedResultDto<LockerRentApplyDto>
            {
                TotalCount = count,
                Items = ObjectMapper.Map<IReadOnlyList<LockerRentApply>, IReadOnlyList<LockerRentApplyDto>>(list)
            };
        }

    }
}
