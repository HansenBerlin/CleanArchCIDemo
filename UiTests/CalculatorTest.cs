using System.Diagnostics.Metrics;
using Bunit;
using CalculatorWebClient;
using CalculatorWebClient.Pages;
using Xunit;

namespace UiTests;

public class CalculatorTest
{
    [Fact]
    public void Test1()
    {
        var del = new delete();
        // Arrange: render the Counter.razor component
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Counter>();

        // Act: find and click the <button> element to increment
        // the counter in the <p> element
        cut.Find("button").Click();

        // Assert: first find the <p> element, then verify its content
        cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
    }
}