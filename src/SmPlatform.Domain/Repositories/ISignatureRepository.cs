using SmPlatform.Domain.DataModels;
using System.Linq.Expressions;

namespace SmPlatform.Domain.Repositories;

/// <summary>
/// 签名仓储
/// </summary>
public interface ISignatureRepository : IRepository<Signature>
{
    /// <summary>
    /// 判断指定签名是否存在
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(Expression<Func<Signature, bool>> value);
}
