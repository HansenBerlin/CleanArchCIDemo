namespace PaymentCore.Interfaces;

public interface IUserSavingsAccount
{
    double Savings { get; set; }
    double MaxSpendingPerDay { get; set; }
    double NegativeAllowed { get; set; }
}