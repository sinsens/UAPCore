using SharedLocker.Domain.SharedLockers;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace SharedLocker.SharedLockers
{
    public class LockerRentInfo : CreationAuditedEntity<Guid>
    {
        public LockerRentInfo()
        {

        }

        public LockerRentInfo(Guid id, Guid lockerId):base(id)
        {
            LockerId = lockerId;
        }

        public Guid LockerId { get; set; }

        public Locker Locker { get; set; }

        public Guid RentId { get; set; }

        public LockerRent LockerRent { get; set; }
    }
}
