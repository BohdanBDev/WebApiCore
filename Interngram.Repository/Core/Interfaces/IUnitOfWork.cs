using System;
using System.Threading.Tasks;
using Interngram.Repository.Core.Interfaces.Repositories;

namespace Interngram.Repository.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IPostRepository Posts { get; }
    
    void Complete();
    Task CompleteAsync();
}