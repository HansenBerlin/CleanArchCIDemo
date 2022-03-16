using System.Security;
using PaymentCore.Policies;

namespace PaymentCore.UseCases;

public interface ICheckPasswordSecurityUseCase
{
    bool IsPasswordCompliantToSecurityRules(string password, PasswordSecurityRules rules);
    string GeneratePasswordHash(string plainPassword);
}