using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SharedLocker.Domain.SharedLockers.Dtos;
using SharedLocker.Permissions;
using SharedLocker.SharedLockers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SharedLocker.Domain.SharedLockers
{
    [Authorize]
    public class LockerRentAppService : CrudAppService<LockerRent, LockerRentDto, Guid, PagedAndSortedRentInfoResultRequestDto, CreateLockerRentDto, CreateLockerRentDto>,
        ILockerRentAppService, IApplicationService
    {
        private readonly LockerRentManager _lockerRentManager;

        public LockerRentAppService(IRepository<LockerRent, Guid> repository, LockerRentManager lockerRentManager) : base(repository)
        {
            _lockerRentManager = lockerRentManager;
        }

        public override async Task<LockerRentDto> GetAsync(Guid id)
        {
            var query = await Repository.GetQueryableAsync();

            var lockerRent = await query
                .Include(x => x.Logs)
                .Include(x => x.RentInfos).ThenInclude(c => c.Locker)
                .FirstOrDefaultAsync(x => x.Id == id);

            return await MapToGetOutputDtoAsync(lockerRent);

        }

        [Authorize(SharedLockerPermissions.LockerRent.Return)]
        public async ValueTask ReturnAsync(Guid rentId, ReturnLockerRentDto input)
        {
            await _lockerRentManager.ReturnAsync(rentId, input.ReturnTime, input.ReturnRemark);
        }

        [Authorize(SharedLockerPermissions.LockerRent.Create)]
        public async ValueTask<LockerRentDto> RentAsync(CreateLockerRentDto input)
        {
            var lockerRent = await _lockerRentManager.RentAsync(input.AppId, input.Name, input.Phone, input.Remark, input.RentTime, input.LockerIds.Distinct());

            return await MapToGetOutputDtoAsync(lockerRent);
        }

        public override async Task<PagedResultDto<LockerRentDto>> GetListAsync(PagedAndSortedRentInfoResultRequestDto input)
        {
            var query = await Repository.GetQueryableAsync();

            var likeFilter = $"%{input.Filter}%";

            query = query
                .WhereIf(input.AppId.HasValue, x => x.AppId == input.AppId)
                .WhereIf(input.Status?.Any() == true, x => input.Status.Contains(x.Status))
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                    x => 
                        EF.Functions.Like(x.Name, likeFilter) || EF.Functions.Like(x.Phone, likeFilter) ||
                        EF.Functions.Like(x.PinyinName, likeFilter)
                    );

            var count = await query.CountAsync();
            var lockerRents = await query.OrderBy(x=> x.Status).ThenByDescending(x=>x.Id).PageBy(input).ToListAsync();

            return new PagedResultDto<LockerRentDto>
            {
                TotalCount = count,
                Items = ObjectMapper.Map<IReadOnlyList<LockerRent>, IReadOnlyList<LockerRentDto>>(lockerRents)
            };
        }

        [Authorize(SharedLockerPermissions.LockerRent.Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            await _lockerRentManager.DeleteAsync(id);
        }

        [Authorize(SharedLockerPermissions.LockerRent.Discard)]
        public async ValueTask DiscardAsync(Guid id)
        {
            await _lockerRentManager.DiscardAsync(id);
        }
    }
}
