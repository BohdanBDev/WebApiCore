using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Interngram.Domain.Exceptions;
using Interngram.Domain.Services.Interfaces;

namespace Interngram.Domain.Services;

public class FirebaseAuthProvider : IAuthProvider
{
    private readonly FirebaseApp _firebaseApp;
    
    public FirebaseAuthProvider(FirebaseApp firebaseApp)
    {
        _firebaseApp = firebaseApp;
    }
    
    public async Task DeleteUserAsync(string userId)
    {
        await FirebaseAuth.DefaultInstance.DeleteUserAsync(userId);
    }
}