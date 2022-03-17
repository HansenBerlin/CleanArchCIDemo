using PaymentCore.Common;
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
    private readonly IUserDeepCopy _deepCopy;
    
    
    public UserEntity()
    {
        //UserSavingsAccount = new SavingsAccountEntity();
        _deepCopy = new UserDeepCopy();
    }

    public void CopyProperties(IUser copyFrom)
    {
        _deepCopy.CopyUserProperties(this, copyFrom);
    }
}