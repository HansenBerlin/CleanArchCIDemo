﻿using PaymentCore.Emuns;
using PaymentCore.Interfaces;

namespace PaymentWebClient.SessionService;

public interface ISessionService
{
    IUser User { get; set; }
    IUserSavingsAccount SavingsAccount { get; set; }
    AuthenticationState UserAuthState { get; set; }
    string UserName { get; set; }

}