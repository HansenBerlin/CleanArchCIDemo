using System;
using System.Threading.Tasks;
using PaymentApplication.Common;
using PaymentCore.Emuns;
using PaymentCore.Interfaces;
using PaymentCore.UseCases;

namespace PaymentConsoleClient.Controller;

public class UserAuthenticationViewViewController : IUserAuthenticationViewController
{
    private readonly IUserAuthenticationInteractor _interactor;
    private readonly IUser _user;
    
    public UserAuthenticationViewViewController(IUserAuthenticationInteractor interactor, IUser user)
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
                return "LOGIN FAILED";
            }

            IUser user = await _interactor.Authenticate(username, pw);
            _user.CopyProperties(user);
            return "LOGIN SUCCESSFUL";
        }
    }

    public async Task<string> ValidateUserRegistrationInput(string username)
    {
        while (true)
        {
            Console.WriteLine("Sicheres Passwort auswählen. \nx zum abbrechen \nEingabe:");
            string pw = Console.ReadLine() ?? string.Empty;
            if (pw == "x")
            {
                return "REGISTRATION CANCELLED BY USER";
            }
            
            var user = await _interactor.Register(pw, username);
            Console.WriteLine(user.AuthState);

            if (user.AuthState == AuthenticationState.InsecurePassword)
            {
                return "PASSWORD NOT SECURE OR CONTAINING FORBIDDEN CHARACTERS";
            }
            else if (user.AuthState == AuthenticationState.LoggedIn)
            {
                Console.WriteLine("Erfolgreich registriert und eingeloggt.");
                _user.CopyProperties(user);
                return "REGISTRATION SUCCESSFUL";
            }
            else
            {
                return $"REGISTRATION FAILED WITH STATUS: {_user.AuthState}";
            }
        }
    }
}