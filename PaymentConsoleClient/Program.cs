using System;
using System.Net.Http;
using System.Threading.Tasks;
using PaymentApplication.Common;
using PaymentApplication.Controller;
using PaymentApplication.Events;
using PaymentApplication.ValueObjects;
using PaymentConsoleClient.Controller;
using PaymentConsoleClient.Enums;
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
        ISecurityPolicyInteractor pwCheck = new PasswordSecurityController();
        IUserAccountInteractor uai = new LoginController(client, ApiStrings.BaseUrl, pwCheck);
        ISavingsAccountInteractor sai = new SavingsAccountController(client, ApiStrings.BaseUrl);

        IUser user = new UserEntity();
        IUserSavingsAccount savingsAcc = new SavingsAccountEntity();
        user.UserSavingsAccount = savingsAcc;
        
        IUserAuthenticationController userAuth = new UserAuthenticationController(uai, user);
        ISavingsAccountInteractionController sacAcc = new SavingsAccountInteractionController(sai, savingsAcc);

        ISelectionValidation selectionValidationController = new SelectionValidationController(user);
        IMenuOptions menuOptions = new MenuOptionsController(selectionValidationController);
        
        MainMenuSelection selection = menuOptions.SelectFromMainMenu();

        while (selection != MainMenuSelection.Cancel)
        {
            if (selection == MainMenuSelection.Register)
            {
                string userName = await userAuth.ValidateUsernameInput();
                if (string.IsNullOrEmpty(userName) == false)
                {
                    user.Name = userName;
                    await userAuth.ValidateUserRegistrationInput(userName);
                    await sacAcc.AddAccount(userName);
                }
            }
            
            if (selection == MainMenuSelection.Login)
            {
                var status = await userAuth.ValidateLoginInput();
                Console.WriteLine(status);
            }

            if (selection == MainMenuSelection.ShowSavingsAccount)
            {
                while (true)
                {
                    SavingsAccountSelection subSelection = menuOptions.SelectFromSavingsAccountMenu();
                    if (subSelection == SavingsAccountSelection.Send)
                    {
                        Console.WriteLine("Send");
                        Console.WriteLine($"Savings:      {savingsAcc.Savings}");
                        Console.WriteLine($"Negative max: {savingsAcc.NegativeAllowed}");
                        Console.WriteLine($"Max per day:  {savingsAcc.MaxSpendingPerDay}");
                    
                    }
                    else if (subSelection == SavingsAccountSelection.Deposit)
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
            selection = menuOptions.SelectFromMainMenu();
        }
    }
}