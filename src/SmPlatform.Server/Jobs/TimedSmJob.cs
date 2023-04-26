
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SmPlatform.Domain.Repositories;
using SmPlatform.Instructure.EntityFramework;
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

        public TimedSmJob(ITimedSmRepository timedSmRepository, IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _timedSmRepository = timedSmRepository;
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.Now;
            var messagesForExcution = await _timedSmRepository.FindAsync(m => m.ScheduledTime <= now);

            var sendingTasks = messagesForExcution
                .Select(async m =>
                {
                    // 多线程发送，同时注意线程安全问题
                    using var scope = _scopeFactory.CreateAsyncScope();
                    var sender = scope.ServiceProvider.GetRequiredService<ISmSender>();

                    return (m.Id, Sended: await sender.SendAsync(_mapper.Map<ShortMessage>(m))); ;
                })
                .Select(task => task.ContinueWith(async t =>
                {
                    if (t.Result.Sended is false)
                        return;

                    using var scope = _scopeFactory.CreateAsyncScope();
                    var repo = scope.ServiceProvider.GetRequiredService<ITimedSmRepository>();

                    await repo.DeleteByIdAsync(task.Result.Id);
                    await repo.UnitWork.SaveEntitiesAsync();

                }, TaskContinuationOptions.OnlyOnRanToCompletion))
                .ToArray();

            await Task.WhenAll(sendingTasks);
        }
    }
}
