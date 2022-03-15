using MySql.Data.MySqlClient;

namespace PaymentInfrastructure.ServicesController;

public class SqlService : ISqlService
{
    public MySqlConnection con { get; }
    public SqlService()
    {
        string cs = @"server=localhost;port=11111;userid=root;password=my-secret-pw;database=payments;";
        con = new MySqlConnection(cs);
    }
}

public interface ISqlService
{
    MySqlConnection con { get; }
}