using App.Commands;
using App.Services.Console;
using App.Services.QrCode;
using App.Validators;
using FluentAssertions;
using NSubstitute;

namespace Tests.Validators;

public class EncodeSmsCommandValidatorTests
{
    [Theory]
    [InlineData("+491701234567", "Hello", "Sms")]
    [InlineData("+491701234567", "Hello", "SmsTo")]
    [InlineData("+491701234567", "Hello", "Sms_iOS")]
    public void EncodeSmsCommand_Should_Be_Valid(string number, string subject, string encoding)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeSmsCommand(qrCodeService, consoleService)
        {
            Number = number,
            Subject = subject,
            Encoding = encoding
        };
        var validator = new EncodeSmsCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("", "Hello", "Sms")]
    [InlineData("+491701234567", "", "Sms")]
    [InlineData("+491701234567", "Hello", "")]
    [InlineData("+491701234567", "Hello", "Mms")]
    public void EncodeSmsCommand_Should_Not_Be_Valid(string number, string subject, string encoding)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeSmsCommand(qrCodeService, consoleService)
        {
            Number = number,
            Subject = subject,
            Encoding = encoding
        };
        var validator = new EncodeSmsCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeFalse();
    }
}