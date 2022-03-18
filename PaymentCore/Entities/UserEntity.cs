using PaymentCore.Emuns;
using PaymentCore.Interfaces;

namespace PaymentCore.Entities;

public class UserEntity : IUser
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PasswordHash { get; set; }
    public IUserSavingsAccount UserSavingsAccount { get; set; } = new SavingsAccountEntity();
    public AuthenticationState AuthState { get; set; }
    

    public void CopyProperties(IUser copyFrom)
    {
        Id = copyFrom.Id;
        Name = copyFrom.Name;
        AuthState = copyFrom.AuthState;
        PasswordHash = copyFrom.PasswordHash;
        UserSavingsAccount.DeepCopy(copyFrom.UserSavingsAccount);
    }
}