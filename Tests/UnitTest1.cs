using GoPipelineDemo;
using Xunit;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Add_ShouldAddBothNumbers_NoCondition()
    {
        var sut = new Calculator();
        var result = sut.Add(1, 2);
        
        Assert.Equal(3, result);
    }
    
    [Theory]
    [InlineData(3)]
    [InlineData(13)]
    [InlineData(305175781)]
    public void CheckPrime_ShouldReturnOnlyIsPrime_WhenValidPrimeNumberIsPassed(long checkNumber)
    {
        var sut = new Calculator();
        var result = sut.CheckPrime(checkNumber);
        
        Assert.Equal(PrimeState.IsPrime, result.Result);
    }
}