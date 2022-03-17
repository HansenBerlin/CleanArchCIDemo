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
    
    public MainMenuSelection SelectFromMainMenu()
    {
        var options = _selectionValidation.LimitOptionsMainMenu().ToArray();
        Console.WriteLine($"Available Options: \n{SplitOptions(options)}");
        while (true)
        {
            int[] allowed = Array.ConvertAll(options, value => (int) value);
            int input = _selectionValidation.RestrictInputToInt(allowed);
            return (MainMenuSelection)input;
        }
    }

    public MainMenuSelection SelectFromRegistrationMenu()
    {
        throw new System.NotImplementedException();
    }

    public MainMenuSelection SelectFromLoginMenu()
    {
        throw new System.NotImplementedException();
    }

    private string SplitOptions(MainMenuSelection[] options)
    {
        return options.Aggregate("", (current, o) 
            => current + $"{(int) o}: {o} \n");
    }
}