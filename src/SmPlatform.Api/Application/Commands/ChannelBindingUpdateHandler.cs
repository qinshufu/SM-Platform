
using AutoMapper;
using MassTransit.Mediator;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;
using SmPlatform.Model.ViewModels;

namespace SmPlatform.ManagementApi.Application.Commands;

/// <summary>
/// 通道与模板签名的绑定修改命令
/// <para>接受模板和签名 ID 的列表，如果已有的绑定不在该列表 ID 中存在，则删除，如果该列表中的 ID 不存在绑定，则添加</para>
/// </summary>
public class ChannelBindingUpdateHandler : MediatorRequestHandler<ChannelBindingUpdateCommand, ApiResult<ChannelInformation>>
{
    private readonly IChannelRepository _channelRepository;

    private readonly IMapper _mapper;

    public ChannelBindingUpdateHandler(IChannelRepository channelRepository, IMapper mapper)
    {
        _channelRepository = channelRepository;
        _mapper = mapper;
    }

    protected override async Task<ApiResult<ChannelInformation>> Handle(ChannelBindingUpdateCommand request, CancellationToken cancellationToken)
    {
        var channel = await _channelRepository.FindByIdAsync(request.Id);

        if (channel is null)
            return ApiResultFactory.Fail<ChannelInformation>("指定 ID 的通道不存在");

        // 删除已有的绑定

        foreach (var t in channel.Templates.ToArray())
        {
            if (request.Templates.Contains(t.Id) is false)
            {
                channel.Templates.Remove(t);
            }
        }

        foreach (var s in channel.Signatures.ToArray())
        {
            if (request.Signatures.Contains(s.Id) is false)
            {
                channel.Signatures.Remove(s);
            }
        }

        // 添加新的绑定

        foreach (var t in request.Templates)
        {
            // 如果不存在模板绑定
            if (channel.Templates.Any(i => i.Id == t) is false)
            {
                channel.Templates.Add(new Template { Id = t });
            }
        }

        foreach (var s in request.Signatures)
        {
            if (channel.Signatures.Any(i => i.Id == s) is false)
            {
                channel.Signatures.Add(new Signature { Id = s });
            }
        }

        await _channelRepository.UnitWork.SaveEntitiesAsync(cancellationToken);

        return ApiResultFactory.Success(_mapper.Map<ChannelInformation>(channel));
    }
}
