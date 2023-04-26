using Microsoft.EntityFrameworkCore;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;
using System.Linq.Expressions;

namespace SmPlatform.Instructure.EntityFramework.Repositories;

public class TemplateRepository : AbstractRepository<Template>, ITemplateRepository
{
    public TemplateRepository(SmsDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> ExistsAsync(Expression<Func<Template, bool>> value) =>
        _dbContext.Set<Template>().AnyAsync(value);
}
