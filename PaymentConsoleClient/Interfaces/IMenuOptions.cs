using PaymentConsoleClient.Enums;

namespace PaymentConsoleClient.Interfaces;

public interface IMenuOptions
{
    MainMenuSelection SelectFromMainMenu();
    MainMenuSelection SelectFromRegistrationMenu();
    MainMenuSelection SelectFromLoginMenu();
}