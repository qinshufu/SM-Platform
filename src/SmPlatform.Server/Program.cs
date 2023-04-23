using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmPlatform.Instructure.EntityFramework;
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