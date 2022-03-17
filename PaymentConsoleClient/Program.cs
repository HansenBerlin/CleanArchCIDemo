using System.Net.Http;
using System.Threading.Tasks;
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
        IAuthenticateUseCase auth = new LoginController(client, ApiStrings.BaseUrl);
        ISecurityPolicyInteractor pwCheck = new PasswordSecurityController();
        IUserAccountInteractor reg = new UserUserAccountController(client, ApiStrings.BaseUrl, pwCheck);

        IUser user = new UserEntity();
        IUserAuthentication userAuth = new UserAuthenticationController(auth, reg, user);

        ISelectionValidation selectionValidationController = new SelectionValidationController(user);
        IMenuOptions menuOptions = new MenuOptionsController(selectionValidationController);
        
        MainMenuSelection selection = menuOptions.SelectFromMainMenu();

        while (selection != MainMenuSelection.Cancel)
        {
            if (selection == MainMenuSelection.Register)
            {
                string userName = await userAuth.CheckForUnusedUsername();
                if (string.IsNullOrEmpty(userName) == false)
                {
                    user.Name = userName;
                    await userAuth.IsNewUserRegisteredWithPasswordCheck(userName);
                }
            }

            if (selection == MainMenuSelection.NewSavingsAccount)
            {
                
                
            }

            selection = menuOptions.SelectFromMainMenu();
            
        }
        
    }
}