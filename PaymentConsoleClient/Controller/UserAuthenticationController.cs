using System;
using System.Threading.Tasks;
using PaymentApplication.Events;
using PaymentCore.Emuns;
using PaymentCore.Interfaces;
using PaymentCore.UseCases;

namespace PaymentConsoleClient.Controller;

public class UserAuthenticationController : IUserAuthentication
{
    private readonly IAuthenticateUseCase _authentication;
    private readonly IUserAccountInteractor _registration;
    private readonly IUser _user;
    
    public UserAuthenticationController(IAuthenticateUseCase authentication, IUserAccountInteractor registration, IUser user)
    {
        _authentication = authentication;
        _registration = registration;
        _user = user;
    }
    
    public async Task<string> CheckForUnusedUsername()
    {
        Console.WriteLine("Neuen Nutzer registrieren. \nx zum Abbrechen oder \nName eingeben: ");
        while (true)
        {
            string? username = Console.ReadLine();
            if (username == "x")
            {
                return string.Empty;
            }
            var userNameAvailable = await _registration.IsNameAvailable(username);
            if (userNameAvailable)
            {
                return username;
            }
            Console.WriteLine("Nutzername bereits vergeben. Bitte einen anderen auswählen.");
        }
    }
    
    public async Task<bool> CheckIfUsernameIsRegistered(string username)
    {
        var userNameAvailable = await _registration.IsNameAvailable(username);
        return !userNameAvailable;
    }

    public async Task<bool> IsNewUserRegisteredWithPasswordCheck(string username)
    {
        while (true)
        {
            Console.WriteLine("Sicheres Passwort auswählen. \nx zum abbrechen \nEingabe:");
            string pw = Console.ReadLine() ?? string.Empty;
            if (pw == "x")
            {
                return false;
            }
            
            var user = await _registration.Register(pw, username);
            Console.WriteLine(user.AuthState);

            if (user.AuthState == AuthenticationState.InsecurePassword)
            {
                Console.WriteLine("Passwort entspricht nicht den Anforderungen.");
            }
            else if (user.AuthState == AuthenticationState.LoggedIn)
            {
                Console.WriteLine("Erfolgreich registriert und eingeloggt.");
                _user.CopyProperties(user);
                
                return true;
            }
            else
            {
                Console.WriteLine($"Das hat nicht geklappt. Status: {_user.AuthState}");
            }
        }
    }
}