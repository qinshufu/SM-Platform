using SmPlatform.Model.DataModels;

namespace SmPlatform.Api.Application.Exceptions;

/// <summary>
/// 实体未找到异常
/// </summary>
public class EntityNotFoundException<T> : RequestException where T : Entity
{
    public EntityNotFoundException(string? message) : base(message, null)
    {
    }
}
