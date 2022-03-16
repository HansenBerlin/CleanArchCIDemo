using PaymentCore.Repositories;
using PaymentCore.Interfaces;
using PaymentInfrastructure.ServicesController;
using MySql.Data.MySqlClient;

namespace PaymentInfrastructure.Repositorys;

public class UserRepository : IUserRepository
{
    private readonly MySqlConnection _connection;
    
    public UserRepository(ISqlService service)
    {
        _connection = service.con;
    }
    
    public async Task<string> GetById(int iD)
    {
        await _connection.OpenAsync();
        string sql = $"USE payments; SELECT * FROM user WHERE id = {iD};";
        await using var cmd = new MySqlCommand(sql, _connection);
        await using var reader = await cmd.ExecuteReaderAsync();
        string returnValue = "";
        while (await reader.ReadAsync())
        {
            returnValue = reader.GetString(0);
        }

        await _connection.CloseAsync();
        return returnValue;
    }
    
    public async Task<int> UpdatePasswordHash(string name, string pwHash)
    {
        await _connection.OpenAsync();
        string sql = $"UPDATE user SET pw_hash = '{pwHash}' WHERE name = '{name}';";
        await using var cmd = new MySqlCommand(sql, _connection);
        var value = await cmd.ExecuteNonQueryAsync();
        await _connection.CloseAsync();
        return value;
    }
    
    public async Task<bool> IsPasswordHashMatching(string name, string pwHash)
    {
        await _connection.OpenAsync();
        string sql = $"SELECT pw_hash FROM user WHERE name = '{name}';";
        await using var cmd = new MySqlCommand(sql, _connection);
        await using var rdr = await cmd.ExecuteReaderAsync();
        string hash = "";

        while (await rdr.ReadAsync())
        {
            hash = rdr.GetString(0);
        }
        await _connection.CloseAsync();
        return hash == pwHash;
    }
    
    public async Task<bool> IsNameExisting(string name)
    {
        await _connection.OpenAsync();
        string sql = $"SELECT name FROM user WHERE name = '{name}';";
        await using var cmd = new MySqlCommand(sql, _connection);
        await using var rdr = await cmd.ExecuteReaderAsync();
        string userName = "";

        while (await rdr.ReadAsync())
        {
            userName = rdr.GetString(0);
        }
        await _connection.CloseAsync();

        return userName == name;
    }
    
    public async Task<int> SetLoginState(string name, bool isLoggedIn)
    {
        await _connection.OpenAsync();
        string sql = $"UPDATE user SET is_logged_in = {(isLoggedIn ? 1 : 0)} WHERE name = '{name}';";
        await using var cmd = new MySqlCommand(sql, _connection);
        var value = await cmd.ExecuteNonQueryAsync();
        await _connection.CloseAsync();
        return value;
    }

    /*
    public async Task<IUser> Login(IUser user)
    {
        await _connection.OpenAsync();
        string sql =
            $"SELECT us.id, savings_amount, max_negative, max_spending_per_day " +
            $"FROM user us JOIN savings_account sa ON us.id = sa.fk_user " +
            $"WHERE us.name = '{user.Name}';";
        
        await using var cmd = new MySqlCommand(sql, _connection);
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            user.Id = reader.GetInt32(0);
            user.UserSavingsAccount.Savings = reader.GetDouble(1);
            user.UserSavingsAccount.NegativeAllowed = reader.GetDouble(2);
            user.UserSavingsAccount.MaxSpendingPerDay = reader.GetDouble(3);
        }
        await _connection.CloseAsync();

        return user;
    }*/

    public async Task<int> AddNewUser(string userName)
    {
        await _connection.OpenAsync();
        string sql = $"INSERT INTO user (name) VALUES ('{userName}');";
        await using var cmd = new MySqlCommand(sql, _connection);
        var value = await cmd.ExecuteNonQueryAsync();
        await _connection.CloseAsync();
        return value;
    }
}