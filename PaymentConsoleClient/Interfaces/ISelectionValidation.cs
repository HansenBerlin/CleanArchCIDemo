using System.Collections.Generic;
using PaymentConsoleClient.Enums;

namespace PaymentConsoleClient.Interfaces;

public interface ISelectionValidation
{
    Dictionary<object, string> LimitMainMenuOptions();
    Dictionary<object, string> LimitUserAccountMenuOptions();
    List<SavingsAccountOptions> LimitOptionsSavingsAccountMenu();   

    int RestrictInputToInt(int[] allowed);

}