
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SmPlatform.Domain.DataModels;
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
        private readonly SmsDbContext _dbContext;

        private readonly IMapper _mapper;

        private readonly IServiceScopeFactory _scopeFactory;

        public TimedSmJob(SmsDbContext dbContext, IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.Now;
            var messagesForExcution = await _dbContext
                .Set<TimedMessage>()
                .Where(m => m.ScheduledTime <= now)
                .ToArrayAsync();

            var sendingTasks = messagesForExcution
                .Select(_mapper.Map<ShortMessage>)
                .Select(m =>
                {
                    // 多线程发送，同时注意线程安全问题
                    using var scope = _scopeFactory.CreateAsyncScope();
                    var sender = scope.ServiceProvider.GetRequiredService<ISmSender>();

                    return sender.SendAsync(m);
                })
                .ToArray();

            await Task.WhenAll(sendingTasks);
        }
    }
}
