using App.Extensions;
using FluentAssertions;

namespace Tests.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData(null, null)]
    [InlineData("abc", "abc")]
    [InlineData("abc", "Abc")]
    [InlineData("abc", "aBc")]
    [InlineData("abc", "abC")]
    [InlineData("abc", "ABC")]
    public void Should_Be_Equals(string left, string right)
    {
        // arrange
        // act
        var areEquals = left.IgnoreEquals(right);

        // assert
        areEquals.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("", null)]
    [InlineData(null, "")]
    [InlineData("abc", "âbc")]
    [InlineData("edf", "èdf")]
    [InlineData("uvw", "ùvw")]
    public void Should_Not_Be_Equals(string left, string right)
    {
        // arrange
        // act
        var areEquals = left.IgnoreEquals(right);

        // assert
        areEquals.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("10.23")]
    [InlineData("10,23")]
    public void Should_Be_Decimal(string val)
    {
        // arrange
        // act
        var isDecimal = val.IsDecimal(out var _);

        // assert
        isDecimal.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("1abc")]
    [InlineData("10;23")]
    [InlineData("10..23")]
    public void Should_Not_Be_Decimal(string val)
    {
        // arrange
        // act
        var isDecimal = val.IsDecimal(out var _);

        // assert
        isDecimal.Should().BeFalse();
    }
}