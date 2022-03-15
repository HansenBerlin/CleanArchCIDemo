namespace PaymentCore.Interfaces;

public interface IPayment
{
    string ToUser { get; set; }
    string FromUser { get; set; }
    double Amount { get; set; }
}