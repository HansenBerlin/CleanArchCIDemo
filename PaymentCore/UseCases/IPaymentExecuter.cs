using PaymentCore.Emuns;
using PaymentCore.Entities;
using PaymentCore.Interfaces;

namespace PaymentCore.UseCases;

public interface IPaymentExecuter
{
    Task<PaymentState> MakePayment(IPayment paymentData, string url);
}