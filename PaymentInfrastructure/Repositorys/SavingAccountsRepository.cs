using MySql.Data.MySqlClient;
using PaymentCore.Entities;
using PaymentCore.Interfaces;
using PaymentCore.Repositories;
using PaymentInfrastructure.ServicesController;

namespace PaymentInfrastructure.Repositorys;

public class SavingAccountsRepository : ISavingsAccountRepository
{
    private readonly MySqlConnection _connection;
    
    public SavingAccountsRepository(ISqlService service)
    {
        _connection = service.con;
    }
    
    public Task<int> AddFunds(int amount)
    {
        throw new NotImplementedException();
    }

    public Task<int> SubstractFunds(int amount)
    {
        throw new NotImplementedException();
    }

    public Task<int> ChangeDailyLimit(int amount)
    {
        throw new NotImplementedException();
    }

    public Task<int> ChangeMaxNegativeLimit(int amount)
    {
        throw new NotImplementedException();
    }

    public async Task<IUserSavingsAccount> AddNewAccount(int initialAmount, string userName)
    {
        await _connection.OpenAsync();
        string sql = $"INSERT INTO savings_account (savings_amount, max_negative, max_spending_per_day, fk_user) " +
                     $"VALUES ({initialAmount}, {initialAmount*2}, 0, '{userName}');" + 
                     $"SELECT * FROM savings_account WHERE id = LAST_INSERT_ID();";
        await using var cmd = new MySqlCommand(sql, _connection);
        await using var reader = await cmd.ExecuteReaderAsync();
        var account = new SavingsAccountEntity();
        while (await reader.ReadAsync())
        {
            account.Id = reader.GetInt32(0);
            account.Savings = reader.GetDouble(1);
            account.NegativeAllowed = reader.GetDouble(2);
            account.MaxSpendingPerDay = reader.GetDouble(3);
        }

        return account;
    }

    public Task<IUserSavingsAccount> DeleteAccount(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<List<IUserSavingsAccount>> GetUserSavingsAccounts(string userName)
    {
        throw new NotImplementedException();
    }
}