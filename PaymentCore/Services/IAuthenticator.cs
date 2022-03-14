using System.Security;
using PaymentCore.Emuns;
using PaymentCore.Entities;

namespace PaymentCore.Services;

public interface IAuthenticator
{
    Task<IUser> Login(string url, string username, SecureString password);
    Task<IUser> Register(string url, string username, SecureString password);
    Task<AuthenticationState> Logout(string url, string username);
}