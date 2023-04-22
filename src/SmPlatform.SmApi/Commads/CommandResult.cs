namespace SmPlatform.SmApi.Commads;

/// <summary>
/// 命令处理接口
/// </summary>
public record CommandResult
{
    /// <summary>
    /// 是否处理成功
    /// </summary>
    public bool Successed { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public object? Data { get; set; }
}
