using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Text;
using SmPlatform.SmApi.Commads;
using MediatR;
using FluentValidation;

namespace SmPlatform.SmApi;

/// <summary>
/// 短信请求处理器
/// </summary>
public class SmRequestHandler
{

    const int MAX_BODY_SIZE = 2 * 1024 * 1024; // 2MB

    private readonly IMediator _mediator;

    private readonly IValidator<SmSendCommand> _smSendValidator;

    private readonly IValidator<SmBatchSendCommand> _smBatchSendValidator;

    public SmRequestHandler(
        IValidator<SmSendCommand> smSendValidator,
        IValidator<SmBatchSendCommand> smBatchSendValidator,
        IMediator mediator)
    {
        _smSendValidator = smSendValidator;
        _smBatchSendValidator = smBatchSendValidator;
        _mediator = mediator;
    }

    public async Task HandleAsync(HttpContext ctx)
    {

        Debug.Assert(ctx.Request.Path == "/send");

        if (ctx.Request.Body.Length > MAX_BODY_SIZE)
        {
            await SetResponseResultAsync(ctx.Response, HttpStatusCode.RequestEntityTooLarge, "过大的请求体");
            return;
        }

        var result = await ctx.Request.BodyReader.ReadAsync();
        var json = Encoding.UTF8.GetString(result.Buffer);

        var (parseSuccess, data) = TryParseRequestParams(json);

        if (parseSuccess)
        {
            var comandResult = data switch
            {
                SmSendCommand command => await _mediator.Send(command),
                SmBatchSendCommand command => await _mediator.Send(command),
                var _ => throw new NotImplementedException() // 这种情况不存在
            };

            var (status, message) = comandResult switch
            {
                { Successed: true } => (HttpStatusCode.OK, "成功"),
                _ => (HttpStatusCode.BadRequest, comandResult.Message)
            };

            await SetResponseResultAsync(ctx.Response, status, message);

            return;
        }

        await SetResponseResultAsync(ctx.Response, HttpStatusCode.BadRequest, "无效的请求参数");

    }

    async Task SetResponseResultAsync(HttpResponse response, HttpStatusCode code, string body)
    {
        response.StatusCode = (int)code;
        await response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(body));
    }

    (bool ParseSuccess, object? Value) TryParseRequestParams(string content)
    {
        var command = default(object);

        try
        {
            var data = JsonSerializer.Deserialize<SmSendCommand>(content);

            if (_smSendValidator.Validate(data).IsValid)
            {
                command = data;
            }
        }
        catch
        {
            try
            {
                var data = JsonSerializer.Deserialize<SmBatchSendCommand>(content);

                if (_smBatchSendValidator.Validate(data).IsValid)
                {
                    command = data;
                }
            }
            catch
            {
                command = null;
            }
        }

        return (command is not null, command);
    }
}
