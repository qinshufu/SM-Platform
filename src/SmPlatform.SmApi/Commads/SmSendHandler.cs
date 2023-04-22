using MediatR;

namespace SmPlatform.SmApi.Commads;

/// <summary>
/// 短信发送命令处理器
/// </summary>
public class SmSendHandler : IRequestHandler<SmSendCommand, CommandResult>
{
    public async Task<CommandResult> Handle(SmSendCommand request, CancellationToken cancellationToken)
    {
        var (valid, msg) = await CheckSendCommandAsync(request);

        return valid switch
        {
            true => await SendSmAsync(request),
            false => new CommandResult { Successed = false, Message = msg }
        };
    }

    private Task SaveMessageAsync(SmSendCommand command) => command switch
    {
        { Timing: null } => SaveMessageToRedisAsync(command),
        { Timing: not null } => SaveMessageToDbAsync(command),
        _ => throw new NotImplementedException() // 永远不会走到这一步
    };

    private async Task<CommandResult> SendSmAsync(SmSendCommand request)
    {
        try
        {
            await SaveMessageAsync(request);
            return new CommandResult { Successed = true, Message = "消息发送成功" };
        }
        catch
        {
            return new CommandResult { Successed = false, Message = "消息发送失败" };
        }
    }

    private Task SaveMessageToRedisAsync(SmSendCommand command)
    {
        throw new NotImplementedException();
    }

    private Task SaveMessageToDbAsync(SmSendCommand command)
    {
        throw new NotImplementedException();
    }

    private Task<(bool Valid, string Message)> CheckSendCommandAsync(SmSendCommand request)
    {
        // TODO
        return Task.FromResult((false, "error"));
    }
}