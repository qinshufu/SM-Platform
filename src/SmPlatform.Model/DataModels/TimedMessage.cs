﻿using System.Collections.Specialized;
using System.Text.Json;

namespace SmPlatform.Model.DataModels;

/// <summary>
/// 定时消息
/// </summary>
public class TimedMessage : Entity
{
    private string _requestParams;

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

    public NameValueCollection RequestParams
    {
        get => JsonSerializer.Deserialize<NameValueCollection>(_requestParams ?? "{}")!;
        set => _requestParams = JsonSerializer.Serialize(value);
    }

    /// <summary>
    /// 是否已发送
    /// </summary>
    public bool Sended { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime SendTime { get; set; }
}
