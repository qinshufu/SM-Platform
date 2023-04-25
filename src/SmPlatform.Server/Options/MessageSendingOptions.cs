namespace SmPlatform.Server.Options
{
    /// <summary>
    /// 消息发送选项
    /// </summary>
    public record MessageSendingOptions
    {
        /// <summary>
        /// 每个通道的最大重试次数
        /// </summary>
        public int MaxRetryCountPerChannel { get; set; }
    }
}