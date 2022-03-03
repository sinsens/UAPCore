using System;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    public class LoginOutput
    {
        public Guid? TenantId { get; set; }

        public Guid? AppId { get; set; }

        public Guid? UserId { get; set; }
        
        public string RawData { get; set; }
    }
}