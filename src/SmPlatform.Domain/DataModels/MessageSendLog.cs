using System.Collections.Specialized;
using System.Text.Json;

namespace SmPlatform.Domain.DataModels;

/// <summary>
/// 消息接收日志
/// </summary>
public record MessageSendLog : Entity
{
    private string _requestParams;

    private string _responseParams;

    /// <summary>
    /// 配置
    /// </summary>
    public Guid Configuration { get; set; }

    /// <summary>
    /// 平台
    /// </summary>
    public Guid Platform { get; set; }

    /// <summary>
    /// 模板
    /// </summary>
    public Guid Template { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    public Guid Signature { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    public NameValueCollection RequestParams
    {
        get => JsonSerializer.Deserialize<NameValueCollection>(_requestParams ?? "{}")!;
        set => JsonSerializer.Serialize(value);
    }

    /// <summary>
    /// 响应参数
    /// </summary>
    public NameValueCollection ResponseParams
    {
        get => JsonSerializer.Deserialize<NameValueCollection>(_responseParams ?? "{}")!;
        set => JsonSerializer.Serialize(value);
    }

    /// <summary>
    /// 错误
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// 耗时
    /// </summary>
    public double ElapsedTime { get; set; }

    /// <summary>
    /// 发送成功？
    /// </summary>
    public bool SendSuccessed { get; set; }

    /// <summary>
    /// 日志 ID
    /// </summary>
    public string ApiLogId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
}
