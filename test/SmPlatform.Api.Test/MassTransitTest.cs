using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmPlatform.Domain;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;
using SmPlatform.ManagementApi.Application.Commands;
using SmPlatform.Model.ViewModels;

namespace SmPlatform.Api.Test;

/// <summary>
/// MassTransit 的接口测试
/// </summary>
public class MassTransitTest
{
    /// <summary>
    /// 测试 MassTransit 中介者消息的发送
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task MediatorSendTest()
    {
        var command = new ChannelAddCommand();
        var channel = new Channel();
        var channelInfo = new ChannelInformation();

        var repositoryMock = new Mock<IChannelRepository>();
        var mapperMock = new Mock<IMapper>();

        mapperMock.Setup(m => m.Map<Channel>(command)).Returns(channel);
        mapperMock.Setup(m => m.Map<ChannelInformation>(channel)).Returns(channelInfo);

        repositoryMock.Setup(m => m.AddAsync(channel, default)).Returns(Task.FromResult(channel));
        repositoryMock.SetupGet(m => m.UnitWork).Returns(new Mock<IUnitWork>().Object);

        var services = new ServiceCollection()
            .AddScoped(ctx => repositoryMock.Object)
            .AddScoped(_ => mapperMock.Object)
            .AddMassTransit(conf => conf.AddMediator(c =>
            {
                c.AddConsumer<ChannelAddHandler>();
            }));

        var result = await services.BuildServiceProvider().GetRequiredService<IScopedMediator>().SendRequest(command);

        Assert.Equal(channel.Id, result.Data!.Id);
    }

}
