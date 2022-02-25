using SharedLocker.Consts;
using SharedLocker.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharedLocker.Domain.SharedLockers.Dtos
{
    [Serializable]
    public class CreateUpdateLockerDto
    {
        /// <summary>
        /// ����
        /// </summary>
        [DisplayName("����")]
        [Required(ErrorMessage = "{0}�Ǳ�����")]
        [StringLength(LockerConst.MaxLockerNameLength, ErrorMessage = "{0}���ܳ���{1}���ַ�����")]
        public string Name { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        [DisplayName("���")]
        [Required(ErrorMessage = "{0}�Ǳ�����")]
        [Range(LockerConst.MinLockerNumber, LockerConst.MaxLockerNumber, ErrorMessage = "{0}Ӧ��{1}-{2}֮��")]
        public int Number { get; set; }

        /// <summary>
        /// ״̬
        /// </summary>
        [DisplayName("״̬")]
        public LockerStatus? Status { get; set; }

        /// <summary>
        /// �Ƿ�����
        /// </summary>

        [DisplayName("�Ƿ�����")]
        public bool IsActive { get; set; }

        /// <summary>
        /// ����Ӧ��
        /// </summary>
        [DisplayName("����Ӧ��")]
        [Required(ErrorMessage = "{0}�Ǳ�����")]
        public Guid? AppId { get; set; }

    }
}