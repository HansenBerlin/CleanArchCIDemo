using PaymentCore.Emuns;
using PaymentCore.Entities;

namespace PaymentApplication.Models;

public class UserModel : IUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
    public IUserSavingsAccount UserSavingsAccount { get; set; }
    public AuthenticationState AuthState { get; set; }
}