namespace PaymentWebClient.SessionService;

public interface ISessionService
{
    string UserName { get; set; }
    bool isLoggedIn { get; set; }
}