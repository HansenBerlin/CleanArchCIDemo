using System.Security;
using PaymentCore.Interfaces;

namespace PaymentCore.UseCases;

public interface IRegisterAccountUseCase
{
    Task<IUser> Register(string password, string username);
    Task<bool> UnRegister(string username);
    Task<bool> IsNameAvailable(string username);
}
