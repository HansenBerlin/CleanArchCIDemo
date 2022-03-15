using PaymentCore.Repositories;
using MySql.Data.MySqlClient;
using PaymentCore.Entities;
using PaymentCore.Interfaces;
using PaymentInfrastructure.ServicesController;

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
        string returnValue = "";
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            returnValue = reader.GetString(0);
        }

        return returnValue;
    }
    
    public void UpdatePasswordHash(int iD, string pwHash)
    {
        _connection.Open();
        string sql = $"UPDATE user SET pw_hash = '{pwHash}' WHERE id = {iD};";
        using var cmd = new MySqlCommand(sql, _connection);
        cmd.ExecuteNonQuery();
    }
    
    public async Task<bool> IsPasswordHashMatching(string name, string pwHash)
    {
        await _connection.OpenAsync();
        string sql = $"SELECT pw_hash FROM user WHERE name = '{name}';";
        await using var cmd = new MySqlCommand(sql, _connection);
        await using MySqlDataReader rdr = cmd.ExecuteReader();
        string hash = "";

        while (await rdr.ReadAsync())
        {
            hash = rdr.GetString(0);
        }
        return hash == pwHash;
    }
    
    public async Task<bool> IsNameExisting(string name)
    {
        await _connection.OpenAsync();
        string sql = $"SELECT name FROM user WHERE name = '{name}';";
        await using var cmd = new MySqlCommand(sql, _connection);
        await using MySqlDataReader rdr = cmd.ExecuteReader();
        string userName = "";

        while (await rdr.ReadAsync())
        {
            userName = rdr.GetString(0);
        }
        return userName == name;
    }
    
    public Task<int> SetLoginState(int iD, bool isLoggedIn)
    {
        _connection.Open();
        string sql = $"UPDATE user SET is_logged_in = {(isLoggedIn ? 1 : 0)} WHERE id = {iD};";
        using var cmd = new MySqlCommand(sql, _connection);
        cmd.ExecuteNonQuery();
        var test = cmd.ExecuteNonQueryAsync();
        return test;
    }

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
        return user;
    }
}