using FluentValidation;
using SmPlatform.SmApi.Commads;

namespace SmPlatform.SmApi.ViewModel;

/// <summary>
/// 短信批量发送命令验证其
/// </summary>
public class SmBatchSendCommandValidator : AbstractValidator<SmBatchSendCommand>
{

    public SmBatchSendCommandValidator(IValidator<SmSendCommand> validator)
    {
        RuleFor(c => c.BatchCode).MinimumLength(8).WithMessage("无效的批次号码");

        RuleForEach(c => c.Commands).Must(i => validator.Validate(i).IsValid).WithMessage("无法的发送短信命令");
    }
}
