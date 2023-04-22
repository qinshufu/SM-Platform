using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SmPlatform.Instructure.EntityFramework;
using SmPlatform.SmApi;
using SmPlatform.SmApi.Commads;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<SmSendHandler>();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddDbContext<SmsDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("default")));

// ÅäÖÃ Autofac 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(configurator =>
{
    configurator.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

    configurator.RegisterAssemblyTypes(typeof(SmsDbContext).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

    //configurator.RegisterAssemblyModules(typeof(Program).Assembly);
}));

var app = builder.Build();

app.MapPost("/send", ctx =>
{
    var handler = ctx.RequestServices.GetRequiredService<SmRequestHandler>();

    return handler.HandleAsync(ctx);
});

app.Run();
