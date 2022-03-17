using System.Net;
using PaymentApplication.ValueObjects;
using PaymentCore.Emuns;
using PaymentCore.Entities;
using PaymentCore.Interfaces;
using PaymentCore.Policies;
using PaymentCore.UseCases;

namespace PaymentApplication.Controller;

public class LoginController : HttpRequestController, IUserAccountInteractor
{
    public LoginController(HttpClient client, string requestBaseUrl) : base(client, requestBaseUrl) { }

    public async Task<IUser> Authenticate(IUser user)
    {
        HttpResponseMessage response = await Client.GetAsync($"{RequestBaseUrl}/{ApiStrings.UserAuthenticate}/{user.Name}/{user.PasswordHash}");
        if (response.IsSuccessStatusCode)
        {
            user = await response.Content.ReadAsAsync<UserEntity>();
        }
        return user;
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

    public async Task<AuthenticationState> Logout(string url, int userId)
    {
        HttpResponseMessage response = await Client.GetAsync($"{RequestBaseUrl}/{url}/{userId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<AuthenticationState>();
        }
        return AuthenticationState.LoggedIn;
    }
}