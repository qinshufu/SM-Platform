using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SmPlatform.Instructure.EntityFramework;
using SmPlatform.SmApi;
using SmPlatform.SmApi.Commads;
using MassTransit;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<SmSendHandler>();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddDbContext<SmsDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("default")));

// ≈‰÷√ Autofac 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(configurator =>
{
    configurator.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

    configurator.RegisterAssemblyTypes(typeof(SmsDbContext).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

    //configurator.RegisterAssemblyModules(typeof(Program).Assembly);
}));

// Masstransit ≈‰÷√
builder.Services.AddOptions<RabbitMqTransportOptions>().BindConfiguration("RabbitMQ");

builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((ctx, c) =>
    {
        var options = ctx.GetRequiredService<IOptions<RabbitMqTransportOptions>>() ?? Options.Create(new RabbitMqTransportOptions());

        c.Host(options.Value.Host, options.Value.VHost, cfg =>
        {
            cfg.Password(options.Value.Pass);
            cfg.Username(options.Value.User);
        });
    });
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.MapPost("/send", ctx =>
{
    var handler = ctx.RequestServices.GetRequiredService<SmRequestHandler>();

    return handler.HandleAsync(ctx);
});

app.Run();
