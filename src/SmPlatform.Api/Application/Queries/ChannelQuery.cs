using AutoMapper;
using SmPlatform.Api.Application.Exceptions;
using SmPlatform.Api.Domain;
using SmPlatform.BuildingBlock.Extensions;
using SmPlatform.Model.DataModels;
using SmPlatform.Model.ViewModels;

namespace SmPlatform.Api.Application.Queries;

/// <summary>
/// 通道查询
/// </summary>
public class ChannelQuery : IChannelQuery
{
    private readonly IChannelRepository _channelRepository;

    private readonly IMapper _mapper;

    private readonly IQueryable<Channel> _channels;

    // TODO 注入相应的 IQueryable<TEntity>
    public ChannelQuery(IChannelRepository channelRepository, IQueryable<Channel> channels, IMapper mapper)
    {
        _channelRepository = channelRepository;
        _mapper = mapper;
        _channels = channels;
    }

    /// <summary>
    /// 获取短信通道的分页
    /// </summary>
    /// <param name="paginationParams"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ApiResult<Pagination<ChannelBasicInformation>>> PaginationAsync(ChannelPaginationParams paginationParams)
    {
        await Task.Yield();

        var result = _channels
            .Where(c => c.CreateTime > paginationParams.StartTime && c.CreateTime < paginationParams.EndTime)
            .OrderByDescending(c => c.CreateTime)
            .Pagination(paginationParams.PageNumber, paginationParams.PageSize);

        return ApiResultFactory.Success(_mapper.Map<Pagination<ChannelBasicInformation>>(result));
    }

    /// <summary>
    /// 根据 id 查询短信通道
    /// </summary>
    /// <param name="id">短信通道 ID</param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException{Channel}">当指定 ID 对应的短信通道不存在时抛出</exception>
    public async Task<ApiResult<ChannelInformation>> QueryByIdAsync(Guid id)
    {
        var channel = await _channelRepository.GetOrDefaultByIdAsync(id) ?? throw new EntityNotFoundException<Channel>("没有找到对应的短信通道");

        return ApiResultFactory.Success(_mapper.Map<ChannelInformation>(channel));
    }
}
