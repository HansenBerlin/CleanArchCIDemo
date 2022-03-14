using System;
using PasswordCheckerLibrary;


namespace CalculatorConsoleClient;

public static class Program
{
    static void Main(string[] args)
    {

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


    }
}