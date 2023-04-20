using SmPlatform.Model.ViewModels;

namespace SmPlatform.Api.Application.Queries;

/// <summary>
/// 通道查询
/// </summary>
public interface IChannelQuery
{
    Task<ApiResult<ChannelInformation>> QueryByIdAsync(Guid id);
}
