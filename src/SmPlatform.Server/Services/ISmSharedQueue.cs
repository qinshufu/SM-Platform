using SmPlatform.Server.Models;

namespace SmPlatform.Server.Services;

/// <summary>
/// 短信队列管理器
/// </summary>
public interface ISmSharedQueue
{
    /// <summary>
    /// 入队
    /// </summary>
    /// <returns></returns>
    Task EnqueueAsync(ShortMessage shortMessage, CancellationToken cancellationToken = default);

    /// <summary>
    /// 入队多条短信
    /// </summary>
    /// <param name="shortMessages"></param>
    /// <returns></returns>
    Task EnqueueRangeAsync(IEnumerable<ShortMessage> shortMessages, CancellationToken cancellationToken = default);

    /// <summary>
    /// 短信出队
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ShortMessage> DequeueAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 短信出队
    /// </summary>
    /// <param name="number"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<ShortMessage>> DequeueRangeAsync(int number, CancellationToken cancellationToken = default);
}
