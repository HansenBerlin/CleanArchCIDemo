namespace PaymentApplication.Common;

public interface ISavingsAccountViewController
{
    Task AddAccount(string username);
    Task<string> SendPayment();
    Task<string> MakeDeposit();
    Task <string> ShowAccountData();
}