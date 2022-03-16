namespace PaymentCore.Policies;

public record PasswordSecurityRules
{
    public int MinLength { get; } = 12;
    public bool LowerUpper { get; } = true;
    public bool Digits { get; } = true;
    public int SpecialChars { get; } = 0;
    public bool NoReservedChars { get; } = true;
}