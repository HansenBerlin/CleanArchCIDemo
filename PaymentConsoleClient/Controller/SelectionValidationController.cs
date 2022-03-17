using System;
using System.Collections.Generic;
using System.Linq;
using PaymentConsoleClient.Enums;
using PaymentConsoleClient.Interfaces;
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

    public List<MainMenuSelection> LimitOptionsMainMenu()
    {
        var allValues = Enum.GetValues(typeof(MainMenuSelection))
            .Cast<MainMenuSelection>()
            .ToList();
        switch (_user.AuthState)
        {
            case AuthenticationState.LoggedIn:
                allValues.RemoveRange(1, 2);
                break;
            case AuthenticationState.LoggedOut:
                allValues.RemoveRange(3, 3);
                break;
        }
        return allValues;
    }

    public List<SavingsAccountSelection> LimitOptionsSavingsAccountMenu()
    {
        var allValues = Enum.GetValues(typeof(SavingsAccountSelection))
            .Cast<SavingsAccountSelection>()
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