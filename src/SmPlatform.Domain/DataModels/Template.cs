using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace SmPlatform.Domain.DataModels;

/// <summary>
/// 模板
/// </summary>
public record Template : Entity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 模板编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public TemplateCategory Category { get; set; }

    public bool VerifyTemplateParams(NameValueCollection @params)
    {
        var paramNames = Regex.Matches(Content, @"{(?<name>\w+)}").Where(m => m.Success).Select(m => m.Groups["name"].Value);
        var customParamNames = @params;

        // 是否传入的参数包含了模板所需的所有参数
        return paramNames.Any(n => customParamNames.Get(n) is null) is false;
    }

}

/// <summary>
/// 模板分类
/// </summary>
public enum TemplateCategory
{
    /// <summary>
    /// 验证码
    /// </summary>
    VerificationCode = 0,

    /// <summary>
    /// 通知
    /// </summary>
    Notification,

    /// <summary>
    /// 推广
    /// </summary>
    Promotion
}