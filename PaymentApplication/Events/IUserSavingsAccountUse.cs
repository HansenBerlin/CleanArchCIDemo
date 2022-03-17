using PaymentCore.Interfaces;
using PaymentCore.Repositories;

namespace PaymentApplication.Events;

public interface IUserSavingsAccountUse
{
     Task<IUserSavingsAccount> GetUserAccount(string username);
     Task<IUserSavingsAccount> AddNewAccount(string username, double initialDeposit);
}