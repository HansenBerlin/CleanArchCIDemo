using PaymentCore.Emuns;
using PaymentCore.Interfaces;

namespace PaymentCore.Entities;

public class UserEntity : IUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
    public IUserSavingsAccount UserSavingsAccount { get; set; }
    public AuthenticationState AuthState { get; set; }

    public UserEntity()
    {
        UserSavingsAccount = new SavingsAccountEntity();
    }
}