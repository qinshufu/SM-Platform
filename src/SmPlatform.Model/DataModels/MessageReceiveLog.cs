using System.Collections.Specialized;
using System.Text.Json;

namespace SmPlatform.Model.DataModels;

/// <summary>
/// 消息接受日志
/// </summary>
public record MessageReceiveLog : Entity
{
    private string _requestParams;

    /// <summary>
    /// 接入平台 id
    /// </summary>
    public Guid Platform { get; set; }

    ///// <summary>
    ///// 接入平台名称
    ///// </summary>
    //public string PlatformName { get; set; }

    /// <summary>
    /// 业务信息
    /// </summary>
    public string Business { get; set; }

    /// <summary>
    /// 配置
    /// </summary>
    public List<Channel> Configurations { get; set; }

    /// <summary>
    /// 模板
    /// </summary>
    public Template Template { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    public Signature Signature { get; set; }

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
    /// 失败信息
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// 耗费时间 秒为单位
    /// </summary>
    public double ElapsedTime { get; set; }

    /// <summary>
    /// api 日志 id
    /// </summary>
    public string ApiLogId { get; set; }

    /// <summary>
    /// 接收成功
    /// </summary>
    public bool ReceiveSuccessed { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
}
