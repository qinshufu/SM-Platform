using FluentValidation;
using MediatR;
using SmPlatform.SmApi.Commads;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmPlatform.SmApi
{
    /// <summary>
    /// 短信处理器
    /// </summary>
    public class SmsSender : ISmsSender
    {
        private readonly IValidator<SmSendCommand> _smSendValidator;

        private readonly IValidator<SmBatchSendCommand> _smBatchSendValidator;

        private readonly IMediator _mediator;

        public SmsSender(
            IValidator<SmSendCommand> smSendValidator,
            IValidator<SmBatchSendCommand> smBatchSendValidator,
            IMediator mediator)
        {
            _smSendValidator = smSendValidator;
            _smBatchSendValidator = smBatchSendValidator;
            _mediator = mediator;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="sm">短信发送命令，应该为 <see cref="SmSendCommand"/> 或者 <see cref="SmBatchSendCommand"/> 类型</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> SendAsync(object sm)
        {
            var comandResult = sm switch
            {
                SmSendCommand command => await _mediator.Send(command),
                SmBatchSendCommand command => await _mediator.Send(command),
                var _ => throw new NotImplementedException() // 这种情况不存在
            };

            var (status, _) = comandResult switch
            {
                { Successed: true } => (HttpStatusCode.OK, "成功"),
                _ => (HttpStatusCode.BadRequest, comandResult.Message)
            };

            return status == HttpStatusCode.OK;
        }

        /// <summary>
        /// 从字符串解析发送短信命令
        /// </summary>
        /// <param name="content"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryParseCommand(string content, out object? command)
        {
            var result = default(object);

            try
            {
                var data = JsonSerializer.Deserialize<SmSendCommand>(content);

                if (_smSendValidator.Validate(data).IsValid)
                {
                    result = data;
                }
            }
            catch
            {
                try
                {
                    var data = JsonSerializer.Deserialize<SmBatchSendCommand>(content);

                    if (_smBatchSendValidator.Validate(data).IsValid)
                    {
                        result = data;
                    }
                }
                catch
                {
                    result = null;
                }
            }

            command = result;
            return result is not null;
        }
    }
}
