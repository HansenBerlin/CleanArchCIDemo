using PaymentCore.Emuns;
using PaymentCore.Interfaces;

namespace PaymentCore.UseCases;

public interface IAuthenticateUseCase
{
    Task<AuthenticationState> Logout(string url, int userId);
    Task<IUser> Authenticate(IUser user);
}