namespace PaymentWebClient.SessionService;

public class SessionService : ISessionService
{
    public string UserName { get; set; } = String.Empty;
    public bool isLoggedIn { get; set; } = true;

}