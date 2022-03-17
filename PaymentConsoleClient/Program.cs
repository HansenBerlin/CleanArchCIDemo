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
        ICheckPasswordSecurityUseCase pwCheck = new PasswordSecurityController();
        IRegisterAccountUseCase reg = new RegisterController(client, ApiStrings.BaseUrl, pwCheck);

        IUser user = new UserEntity();
        IUserIsAuthenticating userAuth = new UserAuthenticationController(auth, reg, user);

        ISelectionValidation selectionValidationController = new SelectionValidationController(user);
        IMenuOptions menuOptions = new MenuOptionsController(selectionValidationController);
        
        var selection = menuOptions.SelectFromMainMenu();

        if (selection == MainMenuSelection.Register)
        {
            string userName = await userAuth.CheckForUnusedUsername();
            if (string.IsNullOrEmpty(userName) == false)
            {
                user.Name = userName;
                await userAuth.IsNewUserRegisteredWithPasswordCheck(userName);
            }
        }

        menuOptions.SelectFromMainMenu();
    }
}