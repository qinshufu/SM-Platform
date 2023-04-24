using AutoMapper;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using SmPlatform.Domain.DataModels;
using SmPlatform.Server.Models;
using SmPlatform.Server.Options;
using SmPlatform.Server.Services;

namespace SmPlatform.Server.Commands
{
    /// <summary>
    /// 即时短信调度处理
    /// </summary>
    public class InstantSmScheduleHandler : MediatorRequestHandler<InstantSmScheduleCommand>
    {
        private readonly IChannelManger _channelManager;

        private readonly ILogger<InstantSmScheduleCommand> _logger;

        private readonly IOptions<MessageSendingOptions> _options;

        private readonly IMapper _mapper;

        public InstantSmScheduleHandler(
            IChannelManger channelManger,
            ILogger<InstantSmScheduleCommand> logger,
            IOptions<MessageSendingOptions> options,
            IMapper mapper)
        {
            _channelManager = channelManger;
            _logger = logger;
            _options = options;
            _mapper = mapper;
        }

        protected override async Task Handle(InstantSmScheduleCommand request, CancellationToken cancellationToken)
        {
            var channels = await _channelManager.GetAllAsync(cancellationToken);

            channels = channels.OrderBy(c => c.Level).ToList();

            var result = channels
                .Select(async c => (Sended: await SendWithRetryAsync(request, c, cancellationToken), Channel: c))
                .FirstOrDefault(task => task.Result.Sended);

            if (result?.Result.Sended is null or false)
            {
                _logger.LogWarning("在尝试了多个通道以后，消息仍未发送成功: \n" + request);
            }
        }

        private async Task<bool> SendWithRetryAsync(InstantSmScheduleCommand request, Channel channel, CancellationToken cancellationToken)
        {
            var sender = await _channelManager.LoadSmSenderAsync(channel);

            var policy = Policy.HandleResult<bool>(i => i).WaitAndRetryAsync(
                _options.Value.MaxRetryCountPerChannel,
                sleepDurationProvider: retryAttempt => TimeSpan.FromMilliseconds(500 * Math.Pow(2, retryAttempt)),
                async (h, n) =>
                {
                    var successed = h.Result is not default(bool);

                    if (successed)
                    {
                        _logger.LogInformation($"第{n}次重复发送消息时成功");
                        await _channelManager.FlagSendingFailedAsync(channel);
                    }
                    else
                    {
                        _logger.LogInformation($"第{n}此发送消息失败");
                        await _channelManager.FlagSendingFailedAsync(channel);
                    }
                });

            var msg = _mapper.Map<ShortMessage>(request);

            msg.Configuration = channel.Id;

            return await policy.ExecuteAsync(() => sender.SendAsync(msg, cancellationToken));
        }
    }
}
