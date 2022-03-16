using System.Security;
using System.Text;
using Newtonsoft.Json;
using PaymentApplication.ValueObjects;
using PaymentCore.Emuns;
using PaymentCore.Entities;
using PaymentCore.Interfaces;
using PaymentCore.Policies;
using PaymentCore.UseCases;

namespace PaymentApplication.Controller;

public class RegisterController : HttpRequestController, IRegisterAccountUseCase
{
    private readonly ICheckPasswordSecurityUseCase _pwCheck;
        
    public RegisterController(HttpClient client, string requestBaseUrl, ICheckPasswordSecurityUseCase pwCheck) : base(client, requestBaseUrl)
    {
        _pwCheck = pwCheck;
    }
    
    public IUser Register(string username)
    {
        /*
        IUser newUser = new UserEntity
        {
            AuthState = AuthenticationState.Registered
        };
        
        foreach (IUser user in _users.Users)
        {
            if (user.Name.Equals(username))
            {
                newUser.AuthState = AuthenticationState.UserAlreadyExists;
                return newUser;
            }
        }

        _users.Users.Add(newUser);
        return newUser;*/
        return null;
    }
    
    
    
    public async Task<IUser> Register(string username, string password)
    {
        IUser user = new UserEntity {AuthState = AuthenticationState.Unregistered, Name = username };
        bool isPasswordSecure = _pwCheck.IsPasswordCompliantToSecurityRules(password, new PasswordSecurityRules());
        if (isPasswordSecure)
        {
            user.PasswordHash = _pwCheck.GeneratePasswordHash(password);
        }
        else
        {
            user.AuthState = AuthenticationState.InsecurePassword;
            return user;
        }

        string uri = $"{RequestBaseUrl}/{ApiStrings.UserRegister}/{user.Name}/{user.PasswordHash}";
        HttpResponseMessage response = await Client.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            user = await response.Content.ReadAsAsync<UserEntity>();
        }
        return user;
    }
    
     

    public Task<bool> UnRegister(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsNameAvailable(string username)
    {
        HttpResponseMessage response = await Client.GetAsync($"{RequestBaseUrl}/{ApiStrings.NameCheck}/{username}");
        bool isNameInUse = false;
        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            isNameInUse = bool.Parse(result);
        }
        Console.WriteLine(!isNameInUse);
        return !isNameInUse;
    }
}