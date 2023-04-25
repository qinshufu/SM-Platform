using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;
using SmPlatform.Server.Models;
using SmPlatform.Server.Options;
using System.Diagnostics;

namespace SmPlatform.Server.Services
{
    /// <summary>
    /// 短信发送器，用于发送短信
    /// </summary>
    public class SmSender : ISmSender
    {
        private readonly ISendLogRepository _sendLogRepository;

        private readonly IMapper _mapper;

        private readonly IOptions<MessageSendingOptions> _options;

        private readonly ILogger<SmSender> _logger;

        private readonly IChannelManger _channelManager;

        public SmSender(
            ISendLogRepository sendLogRepository,
            IMapper mapper,
            IOptions<MessageSendingOptions> options,
            ILogger<SmSender> logger,
            IChannelManger channelManger)
        {
            _sendLogRepository = sendLogRepository;
            _mapper = mapper;
            _options = options;
            _logger = logger;
            _channelManager = channelManger;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <remarks>将从多个通道发送短信，知道发送成功，同时记录通道短信发送成功与失败的次数</remarks>
        /// <param name="shortMessage">短信</param>
        /// <param name="cancellationToken"></param>
        /// <returns>指示短信是否发送成功</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> SendAsync(ShortMessage shortMessage, CancellationToken cancellationToken = default) =>
            LogMessageSendingAsync(m => SendThroughAllChannelsAsync(m, cancellationToken), shortMessage, cancellationToken);

        private async Task<bool> SendThroughAllChannelsAsync(ShortMessage shortMessage, CancellationToken cancellationToken)
        {
            var channels = await _channelManager.GetAllAsync(cancellationToken);

            var sendWithRetry = (Channel channel) => RetryMessageSendingAsync(
                m => SendMessageAsync(channel, m, cancellationToken),
                shortMessage,
                (success, retryCount) => success switch
                {
                    true => _channelManager.FlagSendingSuccessedAsync(channel, cancellationToken),
                    false => _channelManager.FlagSendingFailedAsync(channel, cancellationToken)
                });

            var successTask = channels.Select(c => sendWithRetry(c)).FirstOrDefault(t => t.Result);

            return successTask?.Result is true;
        }

        private async Task<bool> SendMessageAsync(Channel channel, ShortMessage message, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"开始通过 {channel.Name}({channel.Id}) 通道发送消息");

            var platform = await _channelManager.LoadPlatformAsync(channel);
            return await platform.SendMessageAsync(message, cancellationToken);
        }

        private async Task<bool> RetryMessageSendingAsync(
            Func<ShortMessage, Task<bool>> send, ShortMessage message, Func<bool, int, Task> afterRetry)
        {
            var retryNumber = 0;

            var policy = Policy.HandleResult<bool>(i => i).WaitAndRetryAsync(
                _options.Value.MaxRetryCountPerChannel,
                sleepDurationProvider: retryAttempt => TimeSpan.FromMilliseconds(500 * Math.Pow(2, retryAttempt)),
                async (h, n) =>
                {
                    _logger.LogInformation($"第{retryNumber}次消息发送重试: \n" + message);
                    await Task.Delay(n);
                    await afterRetry(h.Result is not default(bool), retryNumber);
                });


            return await policy.ExecuteAsync(() => send(message));
        }

        private async Task<bool> LogMessageSendingAsync(
            Func<ShortMessage, Task<bool>> send, ShortMessage message, CancellationToken cancellationToken)
        {
            var stopWatch = Stopwatch.StartNew();
            var log = _mapper.Map<MessageSendLog>(message);
            var sended = false;

            try
            {
                sended = await send(message);

                log.ElapsedTime = stopWatch.Elapsed.TotalMilliseconds;
            }
            catch (Exception ex)
            {
                log.ElapsedTime = stopWatch.Elapsed.TotalMilliseconds;
                log.Error = ex.ToString();
            }
            finally
            {
                stopWatch.Start();
                await _sendLogRepository.AddAsync(log, cancellationToken);
                await _sendLogRepository.UnitWork.SaveEntitiesAsync(cancellationToken);
            }

            return sended;
        }
    }
}
