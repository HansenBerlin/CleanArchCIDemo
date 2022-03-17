using System.Net;
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

public class UserUserAccountController : HttpRequestController, IUserAccountInteractor
{
    private readonly ISecurityPolicyInteractor _pwCheck;
        
    public UserUserAccountController(HttpClient client, string requestBaseUrl, ISecurityPolicyInteractor pwCheck) : base(client, requestBaseUrl)
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
    
    
    
    public async Task<IUser> Register(string password, string username)
    {
        IUser user = new UserEntity { AuthState = AuthenticationState.Unregistered, Name = username };
        bool isPasswordSecure = _pwCheck.IsPasswordCompliantToSecurityRules(password, new PasswordSecurityRules());
        if (isPasswordSecure)
        {
            string uri = $"{RequestBaseUrl}/{ApiStrings.UserRegister}/{user.Name}/{password}";
            HttpResponseMessage response = await Client.GetAsync(new Uri(uri));
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<UserEntity>();
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                user.AuthState = await response.Content.ReadAsAsync<AuthenticationState>();
            }
            
            return user;
        }

        user.AuthState = AuthenticationState.InsecurePassword;
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