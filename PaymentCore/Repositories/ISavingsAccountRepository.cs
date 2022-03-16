using PaymentCore.Interfaces;

namespace PaymentCore.Repositories;

public interface ISavingsAccountRepository
{
    Task<int> AddFunds(int amount);
    Task<int> SubstractFunds(int amount);
    Task<int> ChangeDailyLimit(int amount);
    Task<int> ChangeMaxNegativeLimit(int amount);
    Task<IUserSavingsAccount> AddNewAccount(int initialAmount, string userName);
    Task<IUserSavingsAccount> DeleteAccount(int accountId);
    Task<List<IUserSavingsAccount>> GetUserSavingsAccounts(string userName);
}