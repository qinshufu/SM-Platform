using FluentValidation;
using SmPlatform.SmApi.Commads;

namespace SmPlatform.SmApi.ViewModel;

/// <summary>
/// 短信发送命令验证器
/// </summary>
public class SmSendCommandValidator : AbstractValidator<SmSendCommand>
{
    public SmSendCommandValidator()
    {
        RuleFor(c => c.Phone).Length(11).Matches($"[0-9]+").WithMessage("无效的电话号码");

        RuleFor(c => c.Template).Must(id => Guid.TryParse(id, out _)).WithMessage("无效的模板 ID");

        RuleFor(c => c.Signature).Must(id => Guid.TryParse(id, out _)).WithMessage("无效的签名 ID");

        RuleFor(c => c.TemplateParams).NotEmpty().NotNull().WithMessage("模板参数不能为空");
    }
}
