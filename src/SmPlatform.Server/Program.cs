using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using SmPlatform.Instructure.EntityFramework;
using SmPlatform.Server.Jobs;
using SmPlatform.Server.Options;
using System.Reflection;

var builder = Host.CreateDefaultBuilder();

builder.UseServiceProviderFactory(new AutofacServiceProviderFactory(b =>
{
    b.RegisterAssemblyTypes(typeof(SmsDbContext).Assembly, typeof(Program).Assembly)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
}));

builder.ConfigureServices(service =>
{
    // 注册 Quartz，Job就不用注册了，因为上面的 Autofac 配置已经做了这个工作
    service.AddQuartz(q =>
    {
        q.AddJob<TimedSmJob>(opt => opt.WithIdentity(nameof(TimedSmJob)));

        q.AddTrigger(ops => ops
            .ForJob(nameof(TimedSmJob))
            .WithIdentity(nameof(TimedSmJob) + "Trigger")
            .WithCronSchedule("* * * ? * *"));

        q.UseMicrosoftDependencyInjectionJobFactory();
    });

    service.AddQuartzServer(opt =>
    {
        opt.WaitForJobsToComplete = true;
    });

    service.AddOptions<MessageSendingOptions>().BindConfiguration(nameof(MessageSendingOptions));

    service.AddDbContext<SmsDbContext>();

    service.AddAutoMapper(typeof(Program).Assembly);

    service.AddMassTransit(c =>
    {
        c.AddMediator();

        c.AddConsumers(Assembly.GetEntryAssembly());

        c.UsingRabbitMq((ctx, rc) =>
        {
            rc.ConfigureEndpoints(ctx);
        });
    });
});

builder.Build().Run();