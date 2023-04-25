namespace SmPlatform.SmApi
{
    /// <summary>
    /// 短信发送接口
    /// </summary>
    public interface ISmsSender
    {
        /// <summary>
        /// 发送短信命令
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        Task<bool> SendAsync(object sm);

        /// <summary>
        /// 解析发送命令参数
        /// </summary>
        /// <param name="content"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryParseCommand(string content, out object? result);
    }
}