using SmPlatform.Domain.DataModels;

namespace SmPlatform.Server.Services
{
    /// <summary>
    /// 用来管理短信通道
    /// </summary>
    public interface IChannelManger
    {
        /// <summary>
        /// 对通带的优先级进行重新排序，当通道失败次数到达制定阈值时，该操作将自动执行
        /// </summary>
        Task ReorderAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取所有通道
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Channel>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 标记通道发送消息失败
        /// </summary>
        /// <returns></returns>
        Task FlagSendingFailedAsync(Channel channel, CancellationToken cancellationToken = default);

        /// <summary>
        /// 标记通道发送消息成功
        /// </summary>
        /// <returns></returns>
        Task FlagSendingSuccessedAsync(Channel channel, CancellationToken cancellationToken = default);

        ///// <summary>
        ///// 根据通道加载短信发送器
        ///// </summary>
        ///// <returns></returns>
        //Task<ISmSender> LoadSmSenderAsync(Channel channel, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据通道加载平台
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IPlatformService> LoadPlatformAsync(Channel channel, CancellationToken cancellationToken = default);
    }
}
