namespace PaymentApplication.Common;

public interface IUserAuthenticationController
{
    Task<string> ValidateUsernameInput();
    Task<bool> ValidateUserRegistrationInput(string username);
    Task<string> ValidateLoginInput();
}