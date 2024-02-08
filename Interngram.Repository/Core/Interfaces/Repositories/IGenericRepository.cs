using System.Collections.Generic;

namespace Interngram.Repository.Core.Interfaces.Repositories;

public interface IGenericRepository<TEntity> : IUpdateRepository<TEntity>, IReadOnlyRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAllAsNoTracking();
}