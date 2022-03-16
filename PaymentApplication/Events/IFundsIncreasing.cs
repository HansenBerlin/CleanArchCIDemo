using PaymentCore.Interfaces;

namespace PaymentApplication.Events;

public interface IFundsIncreasing
{
    double AdaptNegativeAvailable(IUserSavingsAccount savingsAccount);

}