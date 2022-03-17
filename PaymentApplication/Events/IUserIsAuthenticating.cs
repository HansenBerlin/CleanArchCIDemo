using PaymentCore.Interfaces;

namespace PaymentApplication.Events;

public interface IUserIsAuthenticating
{
    Task<string> CheckForUnusedUsername();
    Task<bool> CheckIfUsernameIsRegistered(string username);
    Task <bool> IsNewUserRegisteredWithPasswordCheck(string username);
}