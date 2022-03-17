using System.Threading.Tasks;
using PaymentApplication.Events;
using PaymentCore.Emuns;
using PaymentCore.Interfaces;
using PaymentCore.UseCases;

namespace PaymentConsoleClient.Controller;

public class SavingsAccountUseController : IUserSavingsAccountUse
{
    public Task<IUserSavingsAccount> AddAccount(string username)
    {
        throw new System.NotImplementedException();
    }

    public Task<PaymentState> MakePayment(IPayment paymentData)
    {
        throw new System.NotImplementedException();
    }
}