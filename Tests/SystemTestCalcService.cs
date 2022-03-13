using GoPipelineDemo;
using Xunit;

namespace Tests;

public class SystemTestCalcService
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
    
    [Theory]
    [InlineData(2)]
    [InlineData(100)]
    [InlineData(300000000)]
    public void CheckPrime_ShouldReturnOnlyIsNoPrime_WhenValidNonPrimeNumberIsPassed(long checkNumber)
    {
        var sut = new Calculator();
        var result = sut.CheckPrime(checkNumber);
        
        Assert.Equal(PrimeState.IsNoPrime, result.Result);
    }
}