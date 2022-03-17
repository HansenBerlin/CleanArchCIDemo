using PaymentCore.Emuns;
using PaymentCore.Interfaces;

namespace PaymentCore.UseCases;

public interface ISavingsAccountInteractor
{
    Task<IUserSavingsAccount> AddAccount(string username);
    Task<PaymentState> MakePayment(IPayment paymentData);

}