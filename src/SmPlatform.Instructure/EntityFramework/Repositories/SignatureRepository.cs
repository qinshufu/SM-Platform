using Microsoft.EntityFrameworkCore;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;
using System.Linq.Expressions;

namespace SmPlatform.Instructure.EntityFramework.Repositories;

public class SignatureRepository : AbstractRepository<Signature>, ISignatureRepository
{
    public SignatureRepository(SmsDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> ExistsAsync(Expression<Func<Signature, bool>> value) =>
        _dbContext.Set<Signature>().AnyAsync(value);
}
