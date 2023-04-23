using SmPlatform.Server.Models;

namespace SmPlatform.Server.Services
{
    /// <summary>
    /// 表示可以发送短信
    /// </summary>
    public interface ISmSender
    {
        /// <summary>
        /// 发送短信，同时将记录短信发送日志到数据库
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SendAsync(ShortMessage shortMessage, CancellationToken cancellationToken = default);
    }
}