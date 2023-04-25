using SmPlatform.Domain.DataModels;
using System.Linq.Expressions;

namespace SmPlatform.Domain.Repositories;

/// <summary>
/// 模板仓储
/// </summary>
public interface ITemplateRepository : IRepository<Template>
{
    /// <summary>
    /// 判断指定模板是否存在
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(Expression<Func<Template, bool>> value);
}
