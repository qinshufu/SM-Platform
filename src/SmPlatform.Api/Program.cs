using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmPlatform.Api.Application.Options;
using SmPlatform.Api.Instructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SmsDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("default")));

// ÅäÖÃ Autofac 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(configurator =>
{
    configurator.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

    //configurator.RegisterAssemblyModules(typeof(Program).Assembly);
}));

// ÅäÖÃ AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// ÅäÖÃ MassTransit
builder.Services.AddOptions<RabbitMqOptions>().BindConfiguration(nameof(RabbitMqOptions));

builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((ctx, conf) =>
    {
        var options = ctx.GetRequiredService<IOptions<RabbitMqOptions>>();

        conf.Host(options.Value.Host, options.Value.VirtualHost, c =>
        {
            c.Password(options.Value.Password);
            c.Username(options.Value.Username);
        });
    });

    configurator.AddMediator(null);

    configurator.AddConsumers(typeof(Program).Assembly);
});

var app = builder.Build();

// TODO °²×° Autofac 
// ×¢²á ²Ö´¢·þÎñ

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
