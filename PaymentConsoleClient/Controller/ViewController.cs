using System;
using PaymentConsoleClient.Enums;
using PaymentConsoleClient.Interfaces;
using PaymentConsoleClient.Views;
using PaymentCore.UseCases;
using static System.Console;

namespace PaymentConsoleClient.Controller;

public class ViewController
{
    private readonly IUserAccountInteractor _userAccountInteractor;
    private readonly ISelectionValidation _limitOptions;

    public ViewController(IUserAccountInteractor userAccountInteractor, ISelectionValidation limitOptions)
    {
        _userAccountInteractor = userAccountInteractor;
        _limitOptions = limitOptions;
    }
    public void Start()
    {
        Title = "Payment App";
        RunMainMenu();
    }

    private void RunMainMenu()
    {
        string prompt = @"

██████╗  █████╗ ██╗   ██╗███╗   ███╗███████╗███╗   ██╗████████╗     █████╗ ██████╗ ██████╗ 
██╔══██╗██╔══██╗╚██╗ ██╔╝████╗ ████║██╔════╝████╗  ██║╚══██╔══╝    ██╔══██╗██╔══██╗██╔══██╗
██████╔╝███████║ ╚████╔╝ ██╔████╔██║█████╗  ██╔██╗ ██║   ██║       ███████║██████╔╝██████╔╝
██╔═══╝ ██╔══██║  ╚██╔╝  ██║╚██╔╝██║██╔══╝  ██║╚██╗██║   ██║       ██╔══██║██╔═══╝ ██╔═══╝ 
██║     ██║  ██║   ██║   ██║ ╚═╝ ██║███████╗██║ ╚████║   ██║       ██║  ██║██║     ██║     
╚═╝     ╚═╝  ╚═╝   ╚═╝   ╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝   ╚═╝       ╚═╝  ╚═╝╚═╝     ╚═╝     
                                                                                           

Welcome to the Payment App. What would you like to do?
(Use the arrow keys to cycle through options and press enter to select an option)";

        var options = _limitOptions.LimitMainMenuOptions();
        var mainMenu = new MasterMenu(prompt, options);
        var selectedIndex = (MainMenuOptions)mainMenu.Run();

        switch (selectedIndex)
        {
            case MainMenuOptions.UserAccountOptions:
                ShowUserAccountOptions();
                break;
            case MainMenuOptions.SavingsAccountOptions:
                ShowSavingsAccountOptions();
                break;
            case MainMenuOptions.Exit:
                ExitApplication();
                break;
        }
    }

    private void ExitApplication()
    {
        WriteLine("\nPress any key to exit...");
        ReadKey(true);
        Environment.Exit(0);
    }

    private void ShowUserAccountOptions()
    {
        var options = _limitOptions.LimitUserAccountMenuOptions();
        string prompt = "Account Optionen: ";
        var userAccountMenu = new MasterMenu(prompt, options);
        var selectedIndex = (UserAccountOptions)userAccountMenu.Run();
        
        if (selectedIndex == UserAccountOptions.Back)
            RunMainMenu();
        else if (selectedIndex == UserAccountOptions.Login)
            WriteLine("login");
        else if (selectedIndex == UserAccountOptions.Register)
        {
            _userAccountInteractor.Register("asdasdasd34234234234KHKJHKJHKJHKJH", "User" + Guid.NewGuid());
            WriteLine("register");
        }
        else if (selectedIndex == UserAccountOptions.Logout)
            WriteLine("logout");
        
        RunMainMenu();
    }
    private void ShowSavingsAccountOptions()
    {
        WriteLine("Thats it for now");
    }
}