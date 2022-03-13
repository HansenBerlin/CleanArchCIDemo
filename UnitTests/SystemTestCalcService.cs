using GoPipelineDemo;
using Xunit;

namespace UnitTests;

public class SystemTestCalcService
{
    [Fact]
    public void Add_ShouldAddBothNumbers_NoCondition()
    {
        var sut = new Calculator();
        var result = sut.Add(1, 2);
        
        Assert.Equal(3, result);
    }
    
}