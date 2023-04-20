using System.Text.Json.Serialization;

namespace SmPlatform.Api.Application;

/// <summary>
/// Api 的结果
/// </summary>
public record ApiResult
{
    public string Message { get; init; }

    public bool Successed { get; init; }

    public List<object> Errors { get; init; } = new List<object>(0);

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Data { get; init; }
}

/// <summary>
/// Api 的结果的泛型定义，用于给 Swagger 提供文档
/// </summary>
public record ApiResult<T> : ApiResult
{
}



/// <summary>
/// ApiResult 扩展，用于方便地创建 ApiResult
/// </summary>
public static class ApiResultFactory
{
    /// <summary>
    /// 创建成功的 ApiResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ApiResult<T> Success<T>(T data, string message = "Successed.")
        => new ApiResult<T>() { Data = data, Message = message, Successed = true };

    /// <summary>
    /// 创建失败的 ApiResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ApiResult<T> Fail<T>() => new ApiResult<T>() { Message = "Failed." };

    /// <summary>
    /// 创建成功的 ApiResult 并且不携带数据
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ApiResult SuccessWithoutData(string message = "Successed.") => new ApiResult { Message = message, Successed = true };

    /// <summary>
    /// 创建失败的 ApiResult 并且不携带数据
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ApiResult FailWithoutData(string message = "Failed.") => new ApiResult { Message = message, Successed = false };
}

