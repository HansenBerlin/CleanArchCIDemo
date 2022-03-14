namespace PaymentCore.Emuns;

public enum AuthenticationState
{
    LoggedIn,
    LoggedOut,
    WrongPassword,
    InsecurePassword,
    UserNotFound,
    UserAlreadyExists,
    Unregistered
}