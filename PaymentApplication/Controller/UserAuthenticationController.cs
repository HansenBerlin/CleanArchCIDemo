using System.Net;
using PaymentApplication.ValueObjects;
using PaymentCore.Emuns;
using PaymentCore.Entities;
using PaymentCore.Interfaces;
using PaymentCore.Policies;
using PaymentCore.UseCases;

namespace PaymentApplication.Controller;

public class UserAuthenticationController : HttpRequestController, IUserAuthenticationInteractor
{
    private readonly ISecurityPolicyInteractor _pwCheck;

    public UserAuthenticationController(HttpClient client, string requestBaseUrl, ISecurityPolicyInteractor pwCheck) : base(client, requestBaseUrl)
    {
        _pwCheck = pwCheck;
    }
    
    public async Task<bool> IsNameAvailable(string username)
    {
        HttpResponseMessage response = await Client.GetAsync($"{RequestBaseUrl}/{ApiStrings.NameCheck}/{username.ToLower()}");
        bool isNameInUse = false;
        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            isNameInUse = bool.Parse(result);
        }
        return !isNameInUse;
    }

    public async Task<IUser> Authenticate(string username, string password)
    {
        HttpResponseMessage response = await Client.GetAsync($"{RequestBaseUrl}/{ApiStrings.UserAuthenticate}/{username.ToLower()}/{password}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<UserEntity>();
        }

        var state = await response.Content.ReadAsAsync<AuthenticationState>();
        return new UserEntity { AuthState = state };
    }

    public async Task<IUser> Register(string password, string username)
    {
        IUser user = new UserEntity { AuthState = AuthenticationState.Unregistered, Name = username.ToLower() };
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

    public async Task<AuthenticationState> Logout(string userName)
    {
        HttpResponseMessage response = await Client.GetAsync($"{RequestBaseUrl}/{ApiStrings.UserLogout}/{userName}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<AuthenticationState>();
        }
        return AuthenticationState.LoggedIn;
    }
}