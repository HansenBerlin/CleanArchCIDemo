using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor.Services;
using PaymentCore.Emuns;
using PaymentCore.Interfaces;
using PaymentCore.UseCases;
using PaymentWebClient.Models;
using PaymentWebClient.Shared;
using Xunit;

namespace PaymentWebClient.UiTests;

public class CalculatorTest
{
    [Fact]
    public void Test2()
    {
        using var ctx = new TestContext();
        var mockSessionService = new Mock<ISessionService>();
        var mockSavingsAccountInteractor = new Mock<ISavingsAccountInteractor>();
        var mockUserAuthenticationInteractor = new Mock<IUserAuthenticationInteractor>();
        var mockUser = new Mock<IUser>();
        

        mockUser.Setup(m => m.AuthState).Returns(AuthenticationState.LoggedOut);
        mockSessionService.Setup(m => m.User).Returns(mockUser.Object);

        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        
        ctx.Services.AddSingleton(mockSessionService.Object);
        ctx.Services.AddSingleton(mockSavingsAccountInteractor.Object);
        ctx.Services.AddSingleton(mockUserAuthenticationInteractor.Object);
        ctx.Services.AddMudServices();


        
        var cut = ctx.RenderComponent<MainView>();
        //cut.Find("MudButton").Click();
        //cut.Find("MudChip").MarkupMatches("test");


        
        
    }
}