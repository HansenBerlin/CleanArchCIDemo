using PaymentCore.Emuns;
using PaymentCore.Entities;
using PaymentCore.Interfaces;

namespace PaymentWebClient.SessionService;

public class SessionService : ISessionService
{
    public IUser User { get; set; }
    public IUserSavingsAccount SavingsAccount { get; set; }


    public SessionService()
    {
        SavingsAccount = new SavingsAccountEntity();
        User = new UserEntity();
        User.UserSavingsAccount = SavingsAccount;
    }
}