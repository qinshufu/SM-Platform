using AutoMapper;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;
using SmPlatform.Server.Models;
using System.Diagnostics;

namespace SmPlatform.Server.Services
{
    /// <summary>
    /// 短信发送器，用于发送短信
    /// </summary>
    public class SmSender : ISmSender
    {
        private readonly ISendLogRepository _sendLogRepository;

        private readonly IPlatformService _platformService;

        private readonly IMapper _mapper;

        public SmSender(ISendLogRepository sendLogRepository, IPlatformService platformService, IMapper mapper)
        {
            _sendLogRepository = sendLogRepository;
            _platformService = platformService;
            _mapper = mapper;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="shortMessage">短信</param>
        /// <param name="cancellationToken"></param>
        /// <returns>指示短信是否发送成功</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> SendAsync(ShortMessage shortMessage, CancellationToken cancellationToken = default)
        {
            var log = await LogMessageSendingAsync(m => _platformService.SendMessageAsync(m, cancellationToken), shortMessage);

            await _sendLogRepository.AddAsync(log, cancellationToken);
            await _sendLogRepository.UnitWork.SaveEntitiesAsync(cancellationToken);

            return log.SendSuccessed;
        }

        private async Task<MessageSendLog> LogMessageSendingAsync(Func<ShortMessage, Task<bool>> send, ShortMessage message)
        {
            var stopWatch = Stopwatch.StartNew();
            var log = _mapper.Map<MessageSendLog>(message);

            try
            {
                var sended = await send(message);

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
            }

            return log;
        }
    }
}
