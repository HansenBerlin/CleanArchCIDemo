using PaymentCore.Interfaces;

namespace PaymentApplication.Events;

public interface IUserAuthentication
{
    Task<string> CheckForUnusedUsername();
    Task<bool> CheckIfUsernameIsRegistered(string username);
    Task <bool> IsNewUserRegisteredWithPasswordCheck(string username);
}