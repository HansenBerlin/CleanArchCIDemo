using System.Security;
using PaymentCore.Interfaces;

namespace PaymentCore.UseCases;

public interface IRegisterAccountUseCase
{
    Task<IUser> Register(string username, string password);
    Task<bool> UnRegister(string username);
    Task<bool> IsNameAvailable(string username);
}
