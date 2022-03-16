using System;
using System.Net.Http;
using System.Threading.Tasks;
using PaymentApplication.Controller;
using PaymentApplication.ValueObjects;
using PaymentCore.Emuns;
using PaymentCore.Entities;
using PaymentCore.Interfaces;
using PaymentCore.UseCases;

namespace PaymentConsoleClient;

public static class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();
        IAuthenticateUseCase auth = new LoginController(client, ApiStrings.BaseUrl);

        ICheckPasswordSecurityUseCase pwCheck = new PasswordSecurityController();
        IRegisterAccountUseCase reg = new RegisterController(client, ApiStrings.BaseUrl, pwCheck);

        IUser user = new UserEntity();
        
        Console.WriteLine("Neuen Nutzer registrieren. Name eingeben: ");
        while (true)
        {
            string? username = Console.ReadLine();
            var userNameAvailable = await reg.IsNameAvailable(username);
            if (userNameAvailable)
            {
                bool pwsecure = false;
                while (pwsecure == false)
                {
                    Console.WriteLine("Sicheres Passwort auswählen. Eingeben: ");
                    string pw = Console.ReadLine();
                    
                    user = await reg.Register(username, pw);

                    if (user.AuthState == AuthenticationState.InsecurePassword)
                    {
                        Console.WriteLine("Passwort unsicher");
                    }
                    else if (user.AuthState == AuthenticationState.LoggedIn)
                    {
                        Console.WriteLine("SUCCESS");
                        pwsecure = true;
                    }
                    else
                    {
                        Console.WriteLine($"Something went wrong. Authentication State: {user.AuthState}");
                    }
                }
                break;
            }

            Console.WriteLine("Nutzername bereits vergeben. Bitte einen anderen auswählen.");
        }
        
        Console.WriteLine($"{user.Name} created in database");
        Console.ReadKey();
    }
}