
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SmPlatform.Domain.Repositories;
using SmPlatform.Server.Models;
using SmPlatform.Server.Services;

namespace SmPlatform.Server.Jobs
{
    /// <summary>
    /// 定时消息处理的任务
    /// </summary>
    public class TimedSmJob : IJob
    {
        private readonly ITimedSmRepository _timedSmRepository;

        private readonly IMapper _mapper;

        private readonly IServiceScopeFactory _scopeFactory;

        private readonly ISmSharedQueue _smQueue;

        public TimedSmJob(ITimedSmRepository timedSmRepository, IMapper mapper, IServiceScopeFactory scopeFactory, ISmSharedQueue smQueue)
        {
            _timedSmRepository = timedSmRepository;
            _mapper = mapper;
            _scopeFactory = scopeFactory;
            _smQueue = smQueue;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.Now;
            var messagesForExcution = await _timedSmRepository.FindAsync(m => m.ScheduledTime <= now);

            var sendingTasks = messagesForExcution
                .Select(async m =>
                {
                    await _smQueue.EnqueueAsync(_mapper.Map<ShortMessage>(m));

                    return m.Id;
                })
                .Select(task => task.ContinueWith(async t =>
                {
                    using var scope = _scopeFactory.CreateAsyncScope();
                    var repo = scope.ServiceProvider.GetRequiredService<ITimedSmRepository>();

                    await repo.DeleteByIdAsync(task.Result);
                    await repo.UnitWork.SaveEntitiesAsync();

                }, TaskContinuationOptions.OnlyOnRanToCompletion))
                .ToArray();

            await Task.WhenAll(sendingTasks);
        }
    }
}
