using System.Collections.Generic;
using PaymentConsoleClient.Enums;

namespace PaymentConsoleClient.Interfaces;

public interface ISelectionValidation
{
    List<MainMenuSelection> LimitOptionsMainMenu();
    List<SavingsAccountSelection> LimitOptionsSavingsAccountMenu();   

    int RestrictInputToInt(int[] allowed);

}