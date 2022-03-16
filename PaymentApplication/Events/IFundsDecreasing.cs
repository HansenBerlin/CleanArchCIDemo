using PaymentCore.Interfaces;

namespace PaymentApplication.Events;

public interface IFundsDecreasing
{
    bool IsFundsAvailable(IPayment payment, IUserSavingsAccount account);
    bool IsFundsDailyLimitReached(IPayment payment, IUserSavingsAccount account);
}