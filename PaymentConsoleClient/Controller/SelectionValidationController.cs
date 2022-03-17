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
        var account = _user.UserSavingsAccount;
        switch (_user.AuthState)
        {
            case AuthenticationState.LoggedIn:
                if (account.Id != 0)
                    allValues.RemoveRange(1, 3);
                else
                {
                    allValues.RemoveRange(1, 2);
                    allValues.RemoveAt(2);
                }
                break;
            case AuthenticationState.LoggedOut:
                allValues.RemoveRange(3, 4);
                break;
            default:
                break;
        }

        return allValues;
    }

    public List<SavingsAccountSelection> LimitOptionsSavingsAccountMenu()
    {
        var allValues = Enum.GetValues(typeof(SavingsAccountSelection))
            .Cast<SavingsAccountSelection>()
            .ToList();
        var account = _user.UserSavingsAccount;

        switch (_user.AuthState)
        {
            case AuthenticationState.LoggedIn:
                allValues.RemoveRange(1, 2);
                break;
            case AuthenticationState.LoggedOut:
                allValues.RemoveRange(3, 4);
                break;
            default:
                break;
        }

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