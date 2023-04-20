using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmPlatform.Api.Instructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SmsDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("default")));

// ���� Autofac 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(configurator =>
{
    configurator.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

    //configurator.RegisterAssemblyModules(typeof(Program).Assembly);
}));

// ���� AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// ���� MassTransit
builder.Services.AddOptions<RabbitMqHostSettings>().BindConfiguration(nameof(RabbitMqHostSettings));

builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((ctx, conf) =>
    {
        conf.Host(ctx.GetRequiredService<IOptions<RabbitMqHostSettings>>().Value);
    });

    configurator.AddMediator(null);
});

var app = builder.Build();

// TODO ��װ Autofac 
// ע�� �ִ�����

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
