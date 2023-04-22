using AutoMapper;
using MassTransit;
using MediatR;
using SmPlatform.Domain.Events;
using SmPlatform.Domain.Repositories;
using System.Text.RegularExpressions;

namespace SmPlatform.SmApi.Commads;

/// <summary>
/// 短信发送命令处理器
/// </summary>
public class SmSendHandler : IRequestHandler<SmSendCommand, CommandResult>
{
    private readonly IChannelRepository _channelRepository;

    private readonly ITemplateRepository _templateRepository;

    private readonly ISignatureRepository _signatureRepository;

    private readonly IBlackListRepository _blackListRepository;

    private readonly IMapper _mapper;

    private readonly IBus _bus;

    public SmSendHandler(
        IChannelRepository channelRepository, ITemplateRepository templateRepository,
        ISignatureRepository signatureRepository, IBlackListRepository blackListRepository,
        IMapper mapper, IBus bus)
    {
        _channelRepository = channelRepository;
        _templateRepository = templateRepository;
        _signatureRepository = signatureRepository;
        _blackListRepository = blackListRepository;
        _mapper = mapper;
        _bus = bus;
    }

    public async Task<CommandResult> Handle(SmSendCommand request, CancellationToken cancellationToken)
    {
        var (valid, msg) = await CheckSendCommandAsync(request);

        return valid switch
        {
            true => await SendSmAsync(request),
            false => new CommandResult { Successed = false, Message = msg }
        };
    }

    private async Task<CommandResult> SendSmAsync(SmSendCommand request)
    {
        try
        {
            await _bus.Publish(_mapper.Map<SmsSendingScheduledEvent>(request));
            return new CommandResult { Successed = true, Message = "消息发送成功" };
        }
        catch
        {
            return new CommandResult { Successed = false, Message = "消息发送失败" };
        }
    }

    private async Task<(bool Valid, string Message)> CheckSendCommandAsync(SmSendCommand request)
    {
        var channel = await _channelRepository.FindAsync(c => c.Platform.AccessKeyId == request.AccessKeyId);

        return (channel, request) switch
        {
            (null, _) => (false, "短信通道不存在"),
            ({ IsActive: false }, _) => (false, "短信通道未被启用"),
            (_, { Timing: not null }) when request.Timing - request.CreateTime < TimeSpan.FromSeconds(1) =>
                (false, "作为定时消息，定时发送事件与当前事件太近了"), // 定时消息
            _ when channel.Platform.AccessKeySecret == request.AccessKeySecret => (false, "无效的短信通道访问密钥"),
            _ when Regex.IsMatch(request.Phone, @"1[0-9]{10}") => (false, "无效的电话号码"),
            _ when await _blackListRepository.ExistsAsync(b => b.Account == request.Phone) => (false, "该电话在黑名单中"),
            _ when await _templateRepository.ExistsAsync(t => t.Id == request.Template) is false => (false, "模板不存在"),
            _ when await _signatureRepository.ExistsAsync(s => s.Id == request.Signature) is false => (false, "签名不存在"),
            _ when (await _templateRepository.FindByIdAsync(request.Template))
                !.VerifyTemplateParams(request.TemplateParams) is false => (false, "模板参数不匹配"),
            _ => (true, "有效的短信发送请求")
        };
    }

}