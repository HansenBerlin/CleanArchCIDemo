namespace PaymentApplication.ValueObjects;

public static class ApiStrings
{
    public static string BaseUrl = "https://localhost:7250";
    public static string UserAuthenticate = "Authentication/authenticate";
    public static string UserLogout = "Authentication/logout";
    public static string UserRegister = "Authentication/register";
    public static string NameCheck = "Authentication/users";
    public static string MakePayment = "SavingsAccount/transferfunds";
    public static string Deposit = "SavingsAccount/depositfunds";
    public static string AddNewSavingsAccount = "SavingsAccount/newaccount";
    public static string CheckAccountAvailabilityById = "SavingsAccount/accountbyid";
    public static string CheckAccountAvailabilityByUser = "SavingsAccount/accountbyuser";

}