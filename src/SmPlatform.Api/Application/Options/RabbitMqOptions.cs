namespace SmPlatform.ManagementApi.Application.Options;

/// <summary>
/// RabbitMQ 配置
/// </summary>
public class RabbitMqOptions
{
    public string Host { get; set; } = "127.0.0.1";

    public int Port { get; set; } = 5672;

    public string VirtualHost { get; set; } = "/";

    public string Username { get; set; }

    public string Password { get; set; }

}
