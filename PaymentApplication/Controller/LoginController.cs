using System.Security;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using PaymentApplication.ValueObjects;
using PaymentCore.Emuns;
using PaymentCore.Entities;
using PaymentCore.Interfaces;
using PaymentCore.UseCases;

namespace PaymentApplication.Controller;

public class LoginController : HttpRequestController, IAuthenticateUseCase
{
    public LoginController(HttpClient client, string requestBaseUrl) : base(client, requestBaseUrl) { }

    public async Task<IUser> Authenticate(IUser user)
    {
        HttpResponseMessage response = await Client.GetAsync($"{RequestBaseUrl}/{ApiStrings.UserAuthenticate}/{user.Name}/{user.PasswordHash}");
        if (response.IsSuccessStatusCode)
        {
            //string responseMessage = await response.Content.ReadAsStringAsync();
            //user = JsonConvert.DeserializeObject<IUser>(responseMessage);
            
            user = await response.Content.ReadAsAsync<UserEntity>();
        }
        return user;
    }

    
    public async Task<IUser> Login(string url, string username, SecureString password)
    {
        IUser user = new UserEntity()
        {
           AuthState = AuthenticationState.LoggedOut
        };
        HttpResponseMessage response = await Client.GetAsync($"{RequestBaseUrl}/{url}/{username}");
        if (response.IsSuccessStatusCode)
        {
            //string responseMessage = await response.Content.ReadAsStringAsync();
            //user = JsonConvert.DeserializeObject<IUser>(responseMessage);
            
            user = await response.Content.ReadAsAsync<UserEntity>();
            if (user.Id != 0)
            {
                user.AuthState = AuthenticationState.LoggedIn;
            }
        }
        return user;
    }

    public async Task<IUser> Register(string url, string username, SecureString password)
    {
        IUser user = new UserEntity
        {
            AuthState = AuthenticationState.Unregistered,
            Name = username,
            PasswordHash = GeneratePasswordHash(password)
        };
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8);
        httpContent.Headers.ContentType = new("application/json");
        HttpResponseMessage response = await Client.PostAsync($"{RequestBaseUrl}/{url}", httpContent);
        if (response.IsSuccessStatusCode)
        {
            string responseMessage = await response.Content.ReadAsStringAsync();
            user = JsonConvert.DeserializeObject<IUser>(responseMessage);

        }
        return user;
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

    private string GeneratePasswordHash(SecureString plainPassword)
    {
        byte[] data = Encoding.UTF8.GetBytes(plainPassword.ToString());
        using SHA512 sham = new SHA512Managed();
        return Convert.ToBase64String(sham.ComputeHash(data));
    }
}