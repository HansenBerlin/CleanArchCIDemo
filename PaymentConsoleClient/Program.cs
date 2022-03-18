using System.Net.Http;
using System.Threading.Tasks;
using PaymentApplication.Common;
using PaymentApplication.Controller;
using PaymentApplication.ValueObjects;
using PaymentConsoleClient.Controller;
using PaymentConsoleClient.Interfaces;
using PaymentCore.Entities;
using PaymentCore.Interfaces;
using PaymentCore.UseCases;

namespace PaymentConsoleClient;

public static class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();
        IUser user = new UserEntity();

        ISecurityPolicyInteractor pwCheck = new PasswordSecurityController();
        IUserAuthenticationInteractor uai = new UserAuthenticationController(client, ApiStrings.BaseUrl, pwCheck);
        ISavingsAccountInteractor sai = new SavingsAccountController(client, ApiStrings.BaseUrl);
        ISelectionValidation selectionValidationController = new SelectionValidationController(user);
        IUserAuthenticationViewController userAuth = new UserAuthenticationViewViewController(uai, user);
        ISavingsAccountViewController sacAcc = new SavingsAccountViewController(sai, user, selectionValidationController);

        //IMenuOptions menuOptions = new MenuOptionsController(selectionValidationController);
        
        //MainMenuOptions options = menuOptions.SelectFromMainMenu();

        var viewController = new MainViewController(userAuth, selectionValidationController, sacAcc);
        await viewController.Start();

        /*
        while (options != MainMenuOptions.Cancel)
        {
            if (options == MainMenuOptions.Register)
            {
                string userName = await userAuth.ValidateUsernameInput();
                if (string.IsNullOrEmpty(userName) == false)
                {
                    user.Name = userName;
                    await userAuth.ValidateUserRegistrationInput(userName);
                    await sacAcc.AddAccount(userName);
                }
            }
            
            if (options == MainMenuOptions.Login)
            {
                var status = await userAuth.ValidateLoginInput();
                Console.WriteLine(status);
            }

            if (options == MainMenuOptions.ShowSavingsAccount)
            {
                while (true)
                {
                    SavingsAccountOptions subOptions = menuOptions.SelectFromSavingsAccountMenu();
                    if (subOptions == SavingsAccountOptions.Send)
                    {
                        Console.WriteLine("Send");
                        Console.WriteLine($"Savings:      {savingsAcc.Savings}");
                        Console.WriteLine($"Negative max: {savingsAcc.NegativeAllowed}");
                        Console.WriteLine($"Max per day:  {savingsAcc.MaxSpendingPerDay}");
                    
                    }
                    else if (subOptions == SavingsAccountOptions.Deposit)
                    {
                        Console.WriteLine("Deposit");
                    }
                    else
                    {
                        Console.WriteLine("back");
                        break;
                    }
                }
            }
            options = menuOptions.SelectFromMainMenu();
        }*/
    }
}