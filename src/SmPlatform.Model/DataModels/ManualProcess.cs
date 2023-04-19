using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmPlatform.Model.DataModels;

/// <summary>
/// 人工处理记录
/// </summary>
public class ManualProcess : Entity
{
    private string _channels;

    /// <summary>
    /// 模板
    /// </summary>
    public string Template { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    public Signature Signature { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    public string RequestArguments { get; set; }

    /// <summary>
    /// 通道 ID 集合
    /// </summary>
    public List<string> Channels
    {
        get => JsonSerializer.Deserialize<List<string>>(_channels ?? "[]")!;
        set => JsonSerializer.Serialize(value);
    }

    /// <summary>
    /// 处理状态
    /// </summary>
    public ManualProcessStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
}

/// <summary>
/// 人工处理任务状态
/// </summary>
public enum ManualProcessStatus
{
    /// <summary>
    /// 创建完成
    /// </summary>
    Created = 0,

    /// <summary>
    /// 处理中
    /// </summary>
    Processing,

    /// <summary>
    /// 处理完成
    /// </summary>
    Completed,

    /// <summary>
    /// 处理失败
    /// </summary>
    Failed
}