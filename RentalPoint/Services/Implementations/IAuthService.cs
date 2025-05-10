namespace Services.Implementations;

public interface IAuthService
{
    Task<bool> AuthenticateAsync(string? login, string? password);
}