using System.Threading.Tasks;
using PaymentApplication.Common;
using PaymentApplication.Events;
using PaymentCore.Emuns;
using PaymentCore.Interfaces;
using PaymentCore.UseCases;

namespace PaymentConsoleClient.Controller;

public class SavingsAccountInteractionController : ISavingsAccountInteractionController
{
    private readonly ISavingsAccountInteractor _interactor;
    private readonly IUserSavingsAccount _account;
    public SavingsAccountInteractionController(ISavingsAccountInteractor interactor, IUserSavingsAccount account)
    {
        _interactor = interactor;
        _account = account;
    }

    public async Task AddAccount(string username)
    {
        var acc = await _interactor.AddAccount(username);
        _account.Id = acc.Id;
        _account.Savings = acc.Savings;
        _account.NegativeAllowed = acc.NegativeAllowed;
        _account.MaxSpendingPerDay = acc.MaxSpendingPerDay;
    }
}