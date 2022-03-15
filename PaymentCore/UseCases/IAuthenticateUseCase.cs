using System.Security;
using PaymentCore.Emuns;
using PaymentCore.Entities;
using PaymentCore.Interfaces;

namespace PaymentCore.UseCases;

public interface IAuthenticateUseCase
{
    Task<IUser> Login(string url, string username, SecureString password);
    Task<AuthenticationState> Logout(string url, int userId);
    Task<IUser> Authenticate(IUser user);
}