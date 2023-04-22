using MediatR;

namespace SmPlatform.SmApi.Commads;

/// <summary>
/// 短信批量发送命令处理器
/// </summary>
public class SmBatchSendHandler : IRequestHandler<SmBatchSendCommand, CommandResult>
{
    public Task<CommandResult> Handle(SmBatchSendCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
