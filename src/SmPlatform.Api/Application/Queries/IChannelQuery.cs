using SmPlatform.BuildingBlock.Extensions;
using SmPlatform.Model.ViewModels;

namespace SmPlatform.Api.Application.Queries;

/// <summary>
/// 通道查询
/// </summary>
public interface IChannelQuery
{
    /// <summary>
    /// 获取短信通道的分页
    /// </summary>
    /// <param name="paginationParams"></param>
    /// <returns></returns>
    Task<ApiResult<Pagination<ChannelBasicInformation>>> PaginationAsync(ChannelPaginationParams paginationParams);

    /// <summary>
    /// 根据 id 获取短信通道
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ApiResult<ChannelInformation>> QueryByIdAsync(Guid id);
}
