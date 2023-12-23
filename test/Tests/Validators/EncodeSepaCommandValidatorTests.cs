using App.Commands;
using App.Services.Console;
using App.Services.QrCode;
using App.Validators;
using FluentAssertions;
using NSubstitute;

namespace Tests.Validators;

public class EncodeSepaCommandValidatorTests
{
    [Theory]
    [InlineData("DE33100205000001194700", "BFSWDE33BER", "Xyz", "10,23")]
    [InlineData("DE33100205000001194700", "BFSWDE33BER", "Xyz", "10.23")]
    public void EncodeSepaCommand_Should_Be_Valid(string iban, string bic, string name, string amount)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeSepaCommand(qrCodeService, consoleService)
        {
            Iban = iban,
            Bic = bic,
            Name = name,
            Amount = amount
        };
        var validator = new EncodeSepaCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("", "BFSWDE33BER", "Xyz", "10,23")]
    [InlineData("DE33100205000001194700", "", "Xyz", "10,23")]
    [InlineData("DE33100205000001194700", "BFSWDE33BER", "", "10,23")]
    [InlineData("DE33100205000001194700", "BFSWDE33BER", "Xyz", "")]
    [InlineData("DE33100205000001194700", "BFSWDE33BER", "Xyz", "0")]
    [InlineData("DE33100205000001194700", "BFSWDE33BER", "Xyz", "ABC")]
    [InlineData("DE33100205000001194700", "BFSWDE33BER", "Xyz", "-10")]
    public void EncodeSepaCommand_Should_Not_Be_Valid(string iban, string bic, string name, string amount)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeSepaCommand(qrCodeService, consoleService)
        {
            Iban = iban,
            Bic = bic,
            Name = name,
            Amount = amount
        };
        var validator = new EncodeSepaCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeFalse();
    }
}