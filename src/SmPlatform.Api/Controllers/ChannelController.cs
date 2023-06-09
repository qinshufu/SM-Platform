﻿using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using SmPlatform.ManagementApi.Application;
using SmPlatform.ManagementApi.Application.Commands;
using SmPlatform.ManagementApi.Application.Queries;
using SmPlatform.BuildingBlock.Extensions;
using SmPlatform.Model.ViewModels;

namespace SmPlatform.ManagementApi.Controllers
{
    /// <summary>
    /// 短信通道
    /// </summary>
    [Route("api/channel")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelQuery _channelQuery;

        private readonly IScopedMediator _mediator;

        public ChannelController(IChannelQuery channelQuery, IScopedMediator mediator)
        {
            _channelQuery = channelQuery;
            _mediator = mediator;
        }

        /// <summary>
        /// 根据指定 ID 获取短信通道
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public Task<ApiResult<ChannelInformation>> GetChannel([FromRoute] Guid id) => _channelQuery.QueryByIdAsync(id);

        /// <summary>
        /// 添加短信通道
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ApiResult<ChannelInformation>> AddChannel([FromBody] ChannelAddCommand command) => _mediator.SendRequest(command);


        // TODO 这里好像不需要分页，直接取全部就可以了
        /// <summary>
        /// 获取短信通道分页
        /// </summary>
        /// <remarks>默认按照创建时间降序排列</remarks>
        /// <param name="paginationParams"></param>
        /// <returns></returns>
        [HttpGet("pagination")]
        public Task<ApiResult<Pagination<ChannelBasicInformation>>> Pagination(
            [FromQuery] ChannelPaginationParams paginationParams) => _channelQuery.PaginationAsync(paginationParams);

        /// <summary>
        /// 对短信通道进行重新排序
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("rank")]
        public Task<ApiResult> Rank([FromBody] ChannelRankCommand command) => _mediator.SendRequest(command);

        /// <summary>
        /// 更新短信通道配置
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public Task<ApiResult<ChannelInformation>> Update([FromBody] ChannelUpdateCommand command) => _mediator.SendRequest(command);

        /// <summary>
        /// 更新通道与模板签名绑定
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("binding")]
        public Task<ApiResult<ChannelInformation>> UpdateBinding([FromBody] ChannelBindingUpdateCommand command) => _mediator.SendRequest(command);

        /// <summary>
        /// 删除指定短信通道
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public Task<ApiResult> Delete([FromRoute] ChannelDeleteCommand command) => _mediator.SendRequest(command);
    }
}
