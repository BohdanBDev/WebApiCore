using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Interngram.Repository.Core.Interfaces.Repositories;

public interface IReadOnlyRepository<TEntity> where TEntity : class
{
    TEntity Get(string id);
    Task<TEntity> GetAsync(string id);
    
    IEnumerable<TEntity> GetAll();
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
}