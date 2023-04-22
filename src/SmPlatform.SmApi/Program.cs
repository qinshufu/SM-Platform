using SmPlatform.SmApi;
using SmPlatform.SmApi.Commads;
using SmPlatform.SmApi.ViewModel;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<SmSendCommandValidator>();
builder.Services.AddScoped<SmBatchSendCommandValidator>();
builder.Services.AddScoped<SmSendHandler>();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.MapPost("/send", ctx =>
{
    var handler = ctx.RequestServices.GetRequiredService<SmRequestHandler>();

    return handler.HandleAsync(ctx);
});

app.Run();
