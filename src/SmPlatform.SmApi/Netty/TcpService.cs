using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Text;

namespace SmPlatform.SmApi.Netty;

/// <summary>
/// Netty 服务提供 TCP 接口
/// </summary>
public class TcpService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    private readonly ILogger<TcpService> _logger;

    public TcpService(IServiceScopeFactory scopeFactory, ILogger<TcpService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var main = new MultithreadEventLoopGroup();
        var work = new MultithreadEventLoopGroup();
        var @event = new ManualResetEvent(false);

        stoppingToken.Register(() => @event.Set());

        StartChannelAsync(main, work, stoppingToken)
            .ContinueWith(_ => @event.WaitOne())
            .ContinueWith(_ => Task.WhenAll(main.ShutdownGracefullyAsync(), work.ShutdownGracefullyAsync()))
            .Wait();


        _logger.LogInformation("Netty 服务已经关闭");

        return Task.CompletedTask;
    }

    private async Task StartChannelAsync(
        MultithreadEventLoopGroup main, MultithreadEventLoopGroup work, CancellationToken stoppingToken)
    {
        _logger.LogInformation("启动 Netty");

        var channel = default(IChannel);
        const int port = 19090;

        try
        {
            var bootstrap = new ServerBootstrap()
            .Group(main, work)
            .Channel<TcpServerSocketChannel>()
            .Option(ChannelOption.SoBacklog, 100)
            .Handler(new LoggingHandler("LSIN"))
            .ChildHandler(new ActionChannelInitializer<ISocketChannel>(c =>
            {
                var pipeline = c.Pipeline;
                using var scope = _scopeFactory.CreateAsyncScope();
                var channelHandlers = scope.ServiceProvider.GetRequiredService<IEnumerable<IChannelHandler>>().ToArray();

                pipeline.AddLast(new LoggingHandler("CONN"));
                pipeline.AddLast(new DelimiterBasedFrameDecoder(8192, Delimiters.LineDelimiter()));
                pipeline.AddLast(new StringDecoder(Encoding.UTF8));
                pipeline.AddLast(new StringEncoder(Encoding.UTF8));
                pipeline.AddLast(channelHandlers);
            }));

            channel = await bootstrap.BindAsync(port);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "启动 Nettry 失败");

            throw;
        }


        _logger.LogInformation($"Netty 启动成功: {port} 端口");

        stoppingToken.Register(() => channel.CloseAsync());
    }
}
