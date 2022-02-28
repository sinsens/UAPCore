using SharedLocker.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    [Serializable]
    public class LockerRentDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ����ƴ������ĸ
        /// </summary>
        public string PinyinName { get; set; }

        /// <summary>
        /// ����ȫ��ƴ��
        /// </summary>
        public string FullPinyinName { get; set; }

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? RentTime { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// ����״̬
        /// </summary>
        public LockerRentStatus Status { get; set; }

        /// <summary>
        /// ����״̬
        /// </summary>
        public string StatusDesc { get; set; }

        /// <summary>
        /// �黹ʱ��
        /// </summary>
        public DateTime? ReturnTime { get; set; }

        /// <summary>
        /// �黹��ע
        /// </summary>
        public string ReturnRemark { get; set; }

        /// <summary>
        /// ���� App
        /// </summary>
        public Guid? AppId { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public ICollection<LockerRentLockerDto> Lockers { get; set; } = new List<LockerRentLockerDto>();

        /// <summary>
        /// ��־
        /// </summary>
        public ICollection<LockerRentLogDto> Logs { get; set; } = new List<LockerRentLogDto>();
    }

    /// <summary>
    /// �����
    /// </summary>
    public class LockerRentLockerDto : EntityDto<Guid>
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        public int Number { get; set; }
    }

    /// <summary>
    /// ������־
    /// </summary>
    public class LockerRentLogDto
    {
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }
    }
}