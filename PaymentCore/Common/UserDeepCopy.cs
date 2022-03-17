using PaymentCore.Interfaces;

namespace PaymentCore.Common;

public class UserDeepCopy : IUserDeepCopy
{
    public void CopyUserProperties(IUser copyTo, IUser copyFrom)
    {
        copyTo.Id = copyFrom.Id;
        copyTo.Name = copyFrom.Name;
        copyTo.AuthState = copyFrom.AuthState;
        copyTo.PasswordHash = copyFrom.PasswordHash;
        copyTo.UserSavingsAccount = copyFrom.UserSavingsAccount;
    }
}