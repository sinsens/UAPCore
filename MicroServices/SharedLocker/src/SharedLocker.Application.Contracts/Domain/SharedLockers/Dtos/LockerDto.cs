using SharedLocker.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    [Serializable]
    public class LockerDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// ״̬
        /// </summary>
        public LockerStatus Status { get; set; }

        /// <summary>
        /// ״̬����
        /// </summary>
        public string StatusDesc { get; set; }

        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// ����Ӧ��
        /// </summary>
        public Guid? AppId { get; set; }

        /// <summary>
        /// �����¼
        /// </summary>
        //public ICollection<LockerRentInfoDto> RentInfos { get; set; } = new List<LockerRentInfoDto>();

    }

    /// <summary>
    /// �����¼
    /// </summary>
    public class LockerRentInfoDto
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? RentTime { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// �黹ʱ��
        /// </summary>
        public DateTime? ReturnTime { get; set; }

        /// <summary>
        /// �黹��ע
        /// </summary>
        public string ReturnRemark { get; set; }

        /// <summary>
        /// ��ϵ��
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ״̬
        /// </summary>
        public LockerRentStatus Status { get; set; }

        /// <summary>
        /// ״̬����
        /// </summary>
        public string StatusDesc { get; set; }
    }
}