using System.Net;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/send", async ctx =>
{
    const int MAX_BODY_SIZE = 2 * 1024 * 1024; // 2MB

    if (ctx.Request.Body.Length > MAX_BODY_SIZE)
    {
        ctx.Response.StatusCode = (int)HttpStatusCode.RequestEntityTooLarge;
        return;
    }

    var bodyReader = new StreamReader(ctx.Request.Body);

    if (TryParseRequestParams(ctx, out var @params))
    {
        await SendSmAsync(@params);

        ctx.Response.StatusCode = (int)HttpStatusCode.OK;
        return;
    }

    ctx.Response.StatusCode = (int)HttpStatusCode.BadRequest;
});

Task SendSmAsync(object @params)
{
    throw new NotImplementedException();
}

bool TryParseRequestParams(HttpContext ctx, out object @params)
{
    throw new NotImplementedException();
}

app.Run();
