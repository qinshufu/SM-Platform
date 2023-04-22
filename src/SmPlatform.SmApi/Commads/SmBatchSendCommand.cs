using MediatR;

namespace SmPlatform.SmApi.Commads;

/// <summary>
/// 短信批量发送命令
/// </summary>
public class SmBatchSendCommand : IRequest<CommandResult>
{
    /// <summary>
    /// 短信批量发送命令集合
    /// </summary>
    public List<SmSendCommand> Commands { get; set; }

    /// <summary>
    /// 批次编码
    /// </summary>
    public string BatchCode { get; set; }
}
