using PaymentCore.Interfaces;

namespace PaymentCore.Repositories;

public interface IUserRepository
{
    Task<string> GetById(int iD);
    void UpdatePasswordHash(int iD, string pwHash);
    Task<bool> IsPasswordHashMatching(string name, string pwHash);
    Task<bool> IsNameExisting(string name);
    Task<int> SetLoginState(int iD, bool isLoggedIn);
    Task <IUser> Login(IUser user);
}