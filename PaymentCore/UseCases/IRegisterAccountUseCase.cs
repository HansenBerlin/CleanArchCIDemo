using System.Security;
using PaymentCore.Aggregates;
using PaymentCore.Emuns;
using PaymentCore.Entities;
using PaymentCore.Interfaces;

namespace PaymentCore.UseCases;

public interface IRegisterAccountUseCase
{
    Task<IUser> Register(string url, string username, SecureString password);
    IUser Register(string username);
    Task<IUser> UnRegister(string url, string username);
}

public class RegisterAccount : IRegisterAccountUseCase
{
    private IUserAggregate _users;
    
    public RegisterAccount(IUserAggregate users)
    {
        _users = users;
    }
    
    public IUser Register(string username)
    {
        IUser newUser = new UserEntity
        {
            AuthState = AuthenticationState.Registered
        };
        
        foreach (IUser user in _users.Users)
        {
            if (user.Name.Equals(username))
            {
                newUser.AuthState = AuthenticationState.UserAlreadyExists;
                return newUser;
            }
        }

        _users.Users.Add(newUser);
        return newUser;
    }
    
    public Task<IUser> Register(string url, string username, SecureString password)
    {
        throw new NotImplementedException();
    }

    public Task<IUser> UnRegister(string url, string username)
    {
        throw new NotImplementedException();
    }
}