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
            user = await response.Content.ReadAsAsync<UserEntity>();
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
}