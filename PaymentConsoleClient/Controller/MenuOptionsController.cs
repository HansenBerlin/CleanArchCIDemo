using System;
using System.Linq;
using PaymentConsoleClient.Enums;
using PaymentConsoleClient.Interfaces;

namespace PaymentConsoleClient.Controller;

public class MenuOptionsController : IMenuOptions
{
    private readonly ISelectionValidation _selectionValidation;
    
    public MenuOptionsController(ISelectionValidation selectionValidation)
    {
        _selectionValidation = selectionValidation;
    }
    
    public MainMenuOptions SelectFromMainMenu()
    {
        // var options = _selectionValidation.LimitMainMenuOptions().ToArray();
        // Console.WriteLine($"Available Options: \n{SplitOptions(options)}");
        // while (true)
        // {
        //     int[] allowed = Array.ConvertAll(options, value => (int) value);
        //     int input = _selectionValidation.RestrictInputToInt(allowed);
        //     return (MainMenuOptions)input;
        // }
        return MainMenuOptions.Exit;
    }
    
    public SavingsAccountOptions SelectFromSavingsAccountMenu()
    {
        var options = _selectionValidation.LimitOptionsSavingsAccountMenu().ToArray();
        Console.WriteLine($"Available Options: \n{SplitOptions(options)}");
        while (true)
        {
            int[] allowed = Array.ConvertAll(options, value => (int) value);
            int input = _selectionValidation.RestrictInputToInt(allowed);
            return (SavingsAccountOptions)input;
        }
    }

    public MainMenuOptions SelectFromRegistrationMenu()
    {
        throw new System.NotImplementedException();
    }

    public MainMenuOptions SelectFromLoginMenu()
    {
        throw new System.NotImplementedException();
    }

    private string SplitOptions(MainMenuOptions[] options)
    {
        return options.Aggregate("", (current, o) 
            => current + $"{(int) o}: {o} \n");
    }
    
    private string SplitOptions(SavingsAccountOptions[] options)
    {
        return options.Aggregate("", (current, o) 
            => current + $"{(int) o}: {o} \n");
    }
}