using System.Security;
using System.Security.Cryptography;
using System.Text;
using PasswordCheckerLibrary;
using PaymentCore.Policies;
using PaymentCore.UseCases;

namespace PaymentApplication.Controller;

public class PasswordSecurityController : ICheckPasswordSecurityUseCase
{
    public bool IsPasswordCompliantToSecurityRules(string password, PasswordSecurityRules rules)
    {
        var passwordChecker = new PasswordSecurityChecker(rules.MinLength, rules.SpecialChars, rules.NoReservedChars,
            rules.LowerUpper, rules.Digits);
        return passwordChecker.IsPasswordSecure(password);
    }
    
    
}