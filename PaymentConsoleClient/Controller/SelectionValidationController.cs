using System;
using System.Collections.Generic;
using System.Linq;
using PaymentConsoleClient.Enums;
using PaymentConsoleClient.Interfaces;
using PaymentConsoleClient.ValueObjects;
using PaymentCore.Emuns;
using PaymentCore.Interfaces;

namespace PaymentConsoleClient.Controller;

public class SelectionValidationController : ISelectionValidation
{
    private readonly IUser _user;
    
    public SelectionValidationController(IUser user)
    {
        _user = user;
    }

    public Dictionary<object, string> LimitMainMenuOptions()
    {
        var options = new MenuOptions();
        var allValues = options.MainMenu;

        if (_user.AuthState != AuthenticationState.LoggedIn)
        {
            allValues.Remove(MainMenuOptions.SavingsAccountOptions);
        }
        return allValues;
    }

    public Dictionary<object, string> LimitUserAccountMenuOptions()
    {
        var options = new MenuOptions();
        var allValues = options.UserAccountMenu;

        if (_user.AuthState == AuthenticationState.LoggedIn)
        {
            allValues.Remove(UserAccountOptions.Login);
            allValues.Remove(UserAccountOptions.Register);
        }
        else
        {
            allValues.Remove(UserAccountOptions.Logout);
        }
        return allValues;
    }

    public List<SavingsAccountOptions> LimitOptionsSavingsAccountMenu()
    {
        var allValues = Enum.GetValues(typeof(SavingsAccountOptions))
            .Cast<SavingsAccountOptions>()
            .ToList();
        return allValues;
    }
    
    public int RestrictInputToInt(int[] allowed)
    {
        while (true)
        {
            int.TryParse(Console.ReadLine(), out int input);
            if (allowed.Contains(input))
            {
                return input;
            }
        }
    }
}