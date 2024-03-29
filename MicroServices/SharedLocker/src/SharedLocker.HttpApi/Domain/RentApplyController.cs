﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLocker.Domain.SharedLockers;
using SharedLocker.Domain.SharedLockers.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace SharedLocker.Domain
{
    /// <summary>
    /// 租用申请管理
    /// </summary>
    [Authorize]
    [RemoteService]
    [Route("/api/shared-locker/rent-apply")]
    public class RentApplyController: SharedLockerController, IRentApplyAppService
    {
        private readonly IRentApplyAppService _applyAppService;

        public RentApplyController(IRentApplyAppService applyAppService)
        {
            _applyAppService = applyAppService;
        }

        /// <summary>
        /// 管理作废申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("discard/{id}")]
        public ValueTask DiscardAsync(Guid id, DiscardApplyDto input)
        {
            return _applyAppService.DiscardAsync(id, input);
        }

        /// <summary>
        /// 发起申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("apply")]
        public ValueTask<LockerRentApplyDto> ApplyAsync(CreateLockerRentApplyDto input)
        {
            return _applyAppService.ApplyAsync(input);
        }

        /// <summary>
        /// 审核申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("audit/{id}")]
        public ValueTask AuditAsync(Guid id, AuditRentApplyDto input)
        {
            return _applyAppService.AuditAsync(id, input);
        }

        /// <summary>
        /// 取消申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("cancel/{id}")]
        public ValueTask CancelAsync(Guid id, CancelApplyDto input)
        {
            return _applyAppService.CancelAsync(id, input);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public ValueTask<LockerRentApplyDto> GetAsync(Guid id)
        {
            return _applyAppService.GetAsync(id);
        }
		
		/// <summary>
        /// 获取最新待审核记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("last")]
        public ValueTask<LockerRentApplyDto> GetLastAsync()
        {
            return _applyAppService.GetLastAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getlist")]
        public ValueTask<PagedResultDto<LockerRentApplyDto>> GetListAsync(PagedAndSortedRentApplyRequestDto input)
        {
            return _applyAppService.GetListAsync(input);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getmylist")]
        public ValueTask<PagedResultDto<LockerRentApplyDto>> GetMyListAsync(PagedAndSortedRentApplyCustomerRequestDto input)
        {
            return _applyAppService.GetMyListAsync(input);
        }

        /// <summary>
        /// 分页查询服务中申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("getprocesslist")]
        public ValueTask<PagedResultDto<LockerRentApplyDto>> GetProcessListAsync(PagedAndSortedRentApplyCustomerRequestDto input)
        {
            return _applyAppService.GetProcessListAsync(input);
        }
    }
}
