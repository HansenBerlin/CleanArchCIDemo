using System;
using System.Threading.Tasks;
using PaymentApplication.Common;
using PaymentApplication.Events;
using PaymentCore.Emuns;
using PaymentCore.Interfaces;
using PaymentCore.UseCases;

namespace PaymentConsoleClient.Controller;

public class UserAuthenticationController : IUserAuthenticationController
{
    private readonly IUserAccountInteractor _interactor;
    private readonly IUser _user;
    
    public UserAuthenticationController(IUserAccountInteractor interactor, IUser user)
    {
        _interactor = interactor;
        _user = user;
    }
    
    public async Task<string> ValidateUsernameInput()
    {
        Console.WriteLine("Neuen Nutzer registrieren. \nx zum Abbrechen oder \nName eingeben: ");
        while (true)
        {
            string? username = Console.ReadLine();
            if (username == "x")
            {
                return string.Empty;
            }
            var userNameAvailable = await _interactor.IsNameAvailable(username);
            if (userNameAvailable)
            {
                return username;
            }
            Console.WriteLine("Nutzername bereits vergeben. Bitte einen anderen auswählen.");
        }
    }

    public async Task<string> ValidateLoginInput()
    {
        Console.WriteLine("LOGIN. \nx zum Abbrechen oder \nNutzername und Passwort eingeben: ");
        while (true)
        {
            string? username = Console.ReadLine();
            string? pw = Console.ReadLine();
            if (username == "x" || pw == "x")
            {
                return "Abbruch";
            }

            IUser user = await _interactor.Authenticate(username, pw);
            _user.CopyProperties(user);
            return "SUCCESS";
        }
    }

    public async Task<bool> ValidateUserRegistrationInput(string username)
    {
        while (true)
        {
            Console.WriteLine("Sicheres Passwort auswählen. \nx zum abbrechen \nEingabe:");
            string pw = Console.ReadLine() ?? string.Empty;
            if (pw == "x")
            {
                return false;
            }
            
            var user = await _interactor.Register(pw, username);
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