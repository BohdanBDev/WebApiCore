namespace Interngram.Domain.Services.Interfaces;

public interface IAuthProvider
{
    public Task DeleteUserAsync(string userId);
}