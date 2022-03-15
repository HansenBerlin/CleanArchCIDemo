using PaymentCore.Entities;
using PaymentCore.Interfaces;

namespace PaymentCore.Aggregates;

public interface IUserAggregate
{
    IList<IUser> Users { get; set; }
}

public class UserAggregate : IUserAggregate
{
    public IList<IUser> Users { get; set; }
}