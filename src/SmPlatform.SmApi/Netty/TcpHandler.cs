using DotNetty.Transport.Channels;

namespace SmPlatform.SmApi.Netty;

/// <summary>
/// 处理 TCP 请求
/// </summary>
public class TcpHandler : SimpleChannelInboundHandler<string>
{
    private readonly ISmsSender _sender;

    private readonly ILogger<TcpHandler> _logger;

    public TcpHandler(ISmsSender sender, ILogger<TcpHandler> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    protected override void ChannelRead0(IChannelHandlerContext ctx, string msg)
    {
        _logger.LogInformation("接收到 TCP 消息: " + msg[..Math.Min(10, msg.Length)]);

        _sender.SendAsync(msg).ContinueWith(async t =>
        {
            var message = t switch
            {
                { Status: TaskStatus.RanToCompletion, Result: true } => "successed",
                { Status: TaskStatus.RanToCompletion, Result: false } => "failed",
                _ => "server error",
            };

            await ctx.WriteAndFlushAsync(message);
            await ctx.CloseAsync();
        });
    }
}
