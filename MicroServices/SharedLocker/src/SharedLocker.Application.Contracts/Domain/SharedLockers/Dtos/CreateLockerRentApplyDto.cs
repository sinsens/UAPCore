using SharedLocker.Consts;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    [Serializable]
    public class CreateLockerRentApplyDto
    {
        /// <summary>
        /// ������
        /// </summary>
        [DisplayName("����")]
        [Required(ErrorMessage = "{0}�Ǳ�����")]
        [StringLength(LockerRentConst.MaxLockerRentNameLength, ErrorMessage = "{0}���ܳ���{1}���ַ�����")]
        public string Name { get; set; }

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        [DisplayName("��ϵ�绰")]
        [Required(ErrorMessage = "{0}�Ǳ�����")]
        [StringLength(LockerRentConst.MaxLockerRentPhoneLength, ErrorMessage = "{0}���ܳ���{1}���ַ�����")]
        public string Phone { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [DisplayName("����ʱ��")]
        [Required(ErrorMessage = "{0}�Ǳ�����")]
        public DateTime? RentTime { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [DisplayName("��ע")]
        [StringLength(LockerRentConst.MaxLockerRentRemarkLength, ErrorMessage = "{0}���ܳ���{1}���ַ�����")]
        public string Remark { get; set; }

        /// <summary>
        /// ����Ӧ��
        /// </summary>
        [DisplayName("����Ӧ��")]
        [Required(ErrorMessage = "{0}�Ǳ�����")]
        public Guid? AppId { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [DisplayName("��������")]
        [Required(ErrorMessage = "{0}�Ǳ�����")]
        [Range(1, 99, ErrorMessage = "{0}�ķ�ΧӦ��{1}-{2}֮��")]
        public int ApplyCount { get; set; }
    }
}