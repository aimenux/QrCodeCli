using App.Commands;
using App.Services.Console;
using App.Services.QrCode;
using App.Validators;
using FluentAssertions;
using NSubstitute;

namespace Tests.Validators;

public class EncodeUrlCommandValidatorTests
{
    [Theory]
    [InlineData("https://www.google.com")]
    [InlineData("https://www.amazon.com")]
    public void EncodeUrlCommand_Should_Be_Valid(string url)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeUrlCommand(qrCodeService, consoleService)
        {
            Url = url
        };
        var validator = new EncodeUrlCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("null")]
    public void EncodeUrlCommand_Should_Not_Be_Valid(string url)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeUrlCommand(qrCodeService, consoleService)
        {
            Url = url
        };
        var validator = new EncodeUrlCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeFalse();
    }
}