using PaymentCore.Emuns;

namespace PaymentCore.Entities;

public interface IUser
{
    int Id { get; set; }
    string Name { get; set; }
    IUserSavingsAccount UserSavingsAccount { get; set; }
    AuthenticationState AuthState { get; set; }
}