using Interngram.Repository.Models;

namespace Interngram.Repository.Core.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetUserFilteredAsync(User userFilter, CancellationToken token);
    Task<List<User>> GetUsersByIdsAsync(List<string> ids);
}