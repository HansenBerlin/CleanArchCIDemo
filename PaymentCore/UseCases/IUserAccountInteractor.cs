using System.Security;
using PaymentCore.Emuns;
using PaymentCore.Interfaces;

namespace PaymentCore.UseCases;

public interface IUserAccountInteractor
{
    Task<IUser> Register(string password, string username);
    Task<bool> UnRegister(string username);
    Task<bool> IsNameAvailable(string username);
    Task<AuthenticationState> Logout(string url, int userId);
    Task<IUser> Authenticate(string username, string password);

}
