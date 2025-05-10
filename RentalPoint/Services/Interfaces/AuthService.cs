using Services.Implementations;

namespace Services.Interfaces;

public class AuthService : IAuthService
{
    public Task<bool> AuthenticateAsync(string login, string password)
    {
        throw new NotImplementedException();
    }
}