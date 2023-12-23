using App.Commands;
using App.Services.Console;
using App.Services.QrCode;
using App.Validators;
using FluentAssertions;
using NSubstitute;

namespace Tests.Validators;

public class EncodeMailCommandValidatorTests
{
    [Theory]
    [InlineData("xyz@gmail.com", "abc", "123", "SMTP")]
    [InlineData("xyz@gmail.com", "abc", "123", "MATMSG")]
    [InlineData("xyz@gmail.com", "abc", "123", "MAILTO")]
    public void EncodeMailCommand_Should_Be_Valid(string email, string subject, string message, string encoding)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeMailCommand(qrCodeService, consoleService)
        {
            Email = email,
            Subject = subject,
            Message = message,
            Encoding = encoding
        };
        var validator = new EncodeMailCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("", "abc", "123", "MAILTO")]
    [InlineData(null, "abc", "123", "MAILTO")]
    [InlineData("null", "abc", "123", "MAILTO")]
    [InlineData("xyz.com", "abc", "123", "MAILTO")]
    [InlineData("xyz@gmail.com", "abc", "123", "XYZ")]
    public void EncodeMailCommand_Should_Not_Be_Valid(string email, string subject, string message, string encoding)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeMailCommand(qrCodeService, consoleService)
        {
            Email = email,
            Subject = subject,
            Message = message,
            Encoding = encoding
        };
        var validator = new EncodeMailCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeFalse();
    }
}