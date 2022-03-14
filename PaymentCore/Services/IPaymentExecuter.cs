using PaymentCore.Emuns;
using PaymentCore.Entities;

namespace PaymentCore.Services;

public interface IPaymentExecuter
{
    Task<PaymentState> MakePayment(IPayment paymentData, string url);
}