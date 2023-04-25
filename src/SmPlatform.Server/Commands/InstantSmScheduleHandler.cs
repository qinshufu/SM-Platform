﻿using AutoMapper;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using SmPlatform.Domain.DataModels;
using SmPlatform.Server.Models;
using SmPlatform.Server.Options;
using SmPlatform.Server.Services;

namespace SmPlatform.Server.Commands
{
    /// <summary>
    /// 即时短信调度处理
    /// </summary>
    public class InstantSmScheduleHandler : MediatorRequestHandler<InstantSmScheduleCommand>
    {

        private readonly ISmSender _smSender;

        private readonly ILogger<InstantSmScheduleCommand> _logger;

        private readonly IMapper _mapper;

        public InstantSmScheduleHandler(
            ISmSender smSender,
            ILogger<InstantSmScheduleCommand> logger,
            IMapper mapper)
        {
            _smSender = smSender;
            _logger = logger;
            _mapper = mapper;
        }

        protected override async Task Handle(InstantSmScheduleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("收到即时短信处理请求: \n" + request);

            var msg = _mapper.Map<ShortMessage>(request);

            await _smSender.SendAsync(msg);
        }

    }
}
