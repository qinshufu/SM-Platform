using SmPlatform.Server.Models;

namespace SmPlatform.Server.Services
{
    /// <summary>
    /// 平台服务，代表各种短信平台
    /// </summary>
    public interface IPlatformService
    {
        Task<bool> SendMessageAsync(ShortMessage shortMessage, CancellationToken cancellationToken = default);
    }
}