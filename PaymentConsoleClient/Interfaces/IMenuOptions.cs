using PaymentConsoleClient.Enums;

namespace PaymentConsoleClient.Interfaces;

public interface IMenuOptions
{
    MainMenuOptions SelectFromMainMenu();
    MainMenuOptions SelectFromRegistrationMenu();
    MainMenuOptions SelectFromLoginMenu();
    SavingsAccountOptions SelectFromSavingsAccountMenu();

}