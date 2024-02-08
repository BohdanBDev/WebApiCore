using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interngram.Repository.Core.Interfaces.Repositories;

public interface IUpdateRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
}