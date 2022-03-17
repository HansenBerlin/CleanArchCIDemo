namespace PaymentCore.Interfaces;

public interface IUserDeepCopy
{
    void CopyUserProperties(IUser copyTo, IUser copyFrom);
}