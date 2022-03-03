using System;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos
{
    public class UpdateNameAndPhoneDto
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public bool PhoneIsConfirm { get; set; }
    }
}
