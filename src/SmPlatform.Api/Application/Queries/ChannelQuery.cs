using AutoMapper;
using SmPlatform.Api.Application.Exceptions;
using SmPlatform.Api.Domain;
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

    public ChannelQuery(IChannelRepository channelRepository, IMapper mapper)
    {
        _channelRepository = channelRepository;
        _mapper = mapper;
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
