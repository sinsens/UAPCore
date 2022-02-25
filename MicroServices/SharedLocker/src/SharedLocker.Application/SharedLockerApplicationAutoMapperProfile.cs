using SharedLocker.Domain.SharedLockers;
using SharedLocker.Domain.SharedLockers.Dtos;
using AutoMapper;
using UAP.Shared.Helper;
using SharedLocker.SharedLockers;

namespace SharedLocker;

public class SharedLockerApplicationAutoMapperProfile : Profile
{
    public SharedLockerApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Locker, LockerDto>()
            .ForMember(x => x.StatusDesc, e => e.MapFrom(s => s.Status.GetEnumDescription()));
        CreateMap<LockerRentInfo, LockerRentInfoDto>()
            .ForMember(x => x.Id, e => e.MapFrom(s => s.LockerRent.Id))
            .ForMember(x => x.Name, e => e.MapFrom(s => s.LockerRent.Name))
            .ForMember(x => x.Remark, e => e.MapFrom(s => s.LockerRent.Remark))
            .ForMember(x => x.RentTime, e => e.MapFrom(s => s.LockerRent.RentTime))
            .ForMember(x => x.ReturnRemark, e => e.MapFrom(s => s.LockerRent.ReturnRemark))
            .ForMember(x => x.ReturnTime, e => e.MapFrom(s => s.LockerRent.ReturnTime))
            .ForMember(x => x.Status, e => e.MapFrom(s => s.LockerRent.Status))
            .ForMember(x => x.StatusDesc, e => e.MapFrom(s => s.LockerRent.Status.GetEnumDescription()));
        CreateMap<LockerRentInfo, LockerRentLockerDto>()
            .ForMember(x => x.Id, e => e.MapFrom(s => s.Locker.Id))
            .ForMember(x => x.Name, e => e.MapFrom(s => s.Locker.Name))
            .ForMember(x => x.Number, e => e.MapFrom(s => s.Locker.Number));

        CreateMap<LockerRent, LockerRentDto>()
            .ForMember(x => x.Lockers, e => e.MapFrom(s => s.RentInfos))
			.ForMember(x => x.StatusDesc, e => e.MapFrom(s => s.Status.GetEnumDescription()));

        CreateMap<Locker, LockerRentLockerDto>();
        CreateMap<LockerRentLog, LockerRentLogDto>();
    }
}
