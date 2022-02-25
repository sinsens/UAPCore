using System;
using Volo.Abp.Application.Dtos;

namespace WeChatApp.WeChatAppManagement.Dto
{
    public class GetWeChatAppInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}