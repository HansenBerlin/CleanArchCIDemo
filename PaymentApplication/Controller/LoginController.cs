using System.Security;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using PaymentApplication.Models;
using PaymentCore.Emuns;
using PaymentCore.Entities;
using PaymentCore.Services;

namespace PaymentApplication.Controller;

public class LoginController : HttpRequestController, IAuthenticator
{
    public LoginController(HttpClient client, string requestBaseUrl) : base(client, requestBaseUrl) { }
    
    public async Task<IUser> Login(string url, string username, SecureString password)
    {
        IUser user = new UserModel
        {
           AuthState = AuthenticationState.LoggedOut
        };
        HttpResponseMessage response = await _client.GetAsync($"{_requestBaseUrl}/{url}");
        if (response.IsSuccessStatusCode)
        {
            user = await response.Content.ReadAsAsync<IUser>();
            user.AuthState = AuthenticationState.LoggedIn;
        }
        return user;
    }

    public async Task<IUser> Register(string url, string username, SecureString password)
    {
        IUser user = new UserModel
        {
            AuthState = AuthenticationState.Unregistered,
            Name = username,
            PasswordHash = GeneratePasswordHash(password)
        };
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8);
        httpContent.Headers.ContentType = new("application/json");
        HttpResponseMessage response = await _client.PostAsync($"{_requestBaseUrl}/{url}", httpContent);
        if (response.IsSuccessStatusCode)
        {
            string responseMessage = await response.Content.ReadAsStringAsync();
            user = JsonConvert.DeserializeObject<IUser>(responseMessage);

        }
        return user;
    }

    public async Task<AuthenticationState> Logout(string url, string username)
    {
        HttpResponseMessage response = await _client.GetAsync($"{_requestBaseUrl}/{url}/{username}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<AuthenticationState>();
        }
        return AuthenticationState.LoggedIn;
    }

    private string GeneratePasswordHash(SecureString plainPassword)
    {
        byte[] data = Encoding.UTF8.GetBytes(plainPassword.ToString());
        using SHA512 sham = new SHA512Managed();
        return Convert.ToBase64String(sham.ComputeHash(data));
    }
}