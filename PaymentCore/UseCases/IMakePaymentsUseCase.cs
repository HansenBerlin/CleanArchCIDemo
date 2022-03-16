using PaymentCore.Emuns;
using PaymentCore.Interfaces;

namespace PaymentCore.UseCases;

public interface IMakePaymentsUseCase
{
    Task<PaymentState> MakePayment(IPayment paymentData);

}