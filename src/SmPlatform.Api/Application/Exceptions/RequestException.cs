namespace SmPlatform.ManagementApi.Application.Exceptions;

/// <summary>
/// 请求错误
/// </summary>
public class RequestException : Exception
{
    public RequestException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
