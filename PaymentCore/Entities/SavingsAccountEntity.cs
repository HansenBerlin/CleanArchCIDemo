using PaymentCore.Interfaces;

namespace PaymentCore.Entities;

public class SavingsAccountEntity : IUserSavingsAccount
{
    public int Id { get; set; }
    public double Savings { get; set; }
    public double MaxSpendingPerDay { get; set; }
    public double NegativeAllowed { get; set; }
}