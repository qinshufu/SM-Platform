using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using SmPlatform.Api.Application;
using SmPlatform.Api.Application.Commands;
using SmPlatform.Api.Application.Queries;
using SmPlatform.BuildingBlock.Extensions;
using SmPlatform.Model.ViewModels;
using System.Collections.Specialized;

namespace SmPlatform.Api.Controllers
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

        /// <summary>
        /// 获取短信通道分页
        /// </summary>
        /// <remarks>默认按照创建时间降序排列</remarks>
        /// <param name="paginationParams"></param>
        /// <returns></returns>
        [HttpGet("pagination")]
        public Task<ApiResult<Pagination<ChannelBasicInformation>>> Pagination(
            [FromQuery] ChannelPaginationParams paginationParams) => _channelQuery.PaginationAsync(paginationParams);
    }
}
