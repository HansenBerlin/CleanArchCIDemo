using System;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PasswordCheckerLibrary;
using PaymentApplication.Controller;
using PaymentCore.Aggregates;
using PaymentCore.Entities;
using PaymentCore.Interfaces;
using PaymentCore.UseCases;
using PaymentInfrastructure.ServicesController;

namespace PaymentConsoleClient;

public static class Program
{
    private static string GeneratePasswordHash(string plainPassword)
    {
        byte[] data = Encoding.UTF8.GetBytes(plainPassword);
        using SHA512 sham = new SHA512Managed();
        return Convert.ToBase64String(sham.ComputeHash(data));
    }
    
    static async Task Main(string[] args)
    {
        
        //var user = service.Users.GetUserById(2);
        //Console.WriteLine(user);

        string hashed = GeneratePasswordHash("hannes1234");
        //service.Users.UpdateUserPasswordHash(1, hashed);

        //bool isSame = service.Users.IsUserPasswordHashMatch(1, hashed);
        //Console.WriteLine(isSame);

        //bool isExisiting = service.Users.IsUserNameExisting("Hannes");
        //Console.WriteLine(isExisiting);

        var client = new HttpClient();
        IAuthenticateUseCase req = new LoginController(client, "https://localhost:7250");
        IUser user = new UserEntity()
        {
            Name = "Hannes",
            PasswordHash = hashed
        };
        user = await req.Authenticate(user);
        Console.WriteLine(user.UserSavingsAccount.Savings);

        /*

        var pw = new PasswordSecurityChecker();
        bool isSecure = pw.IsPasswordSecure("hello");
        Console.WriteLine(isSecure);

        var pw2 = new PasswordSecurityChecker(10);
        isSecure = pw2.IsPasswordSecure("hellosdfsdf");
        Console.WriteLine(isSecure);

        var pw3 = new PasswordSecurityChecker(specialCharactersNeeded:2);
        isSecure = pw3.IsPasswordSecure("h&%lo");
        Console.WriteLine(isSecure);

        var pw4 = new PasswordSecurityChecker(reservedCharactersForbidden:true);
        isSecure = pw4.IsPasswordSecure("h//lo");
        Console.WriteLine(isSecure);

        var pw5 = new PasswordSecurityChecker(mustContainUpperAndLowerCase:true);
        isSecure = pw5.IsPasswordSecure("h//lo");
        Console.WriteLine(isSecure);

        var pw6 = new PasswordSecurityChecker(mustContainDigits:true);
        isSecure = pw6.IsPasswordSecure("h//lo");
        Console.WriteLine(isSecure);
        
        */


    }
}