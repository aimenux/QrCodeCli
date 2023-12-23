using App.Commands;
using App.Services.Console;
using App.Services.QrCode;
using App.Validators;
using FluentAssertions;
using NSubstitute;

namespace Tests.Validators;

public class DecodeCommandValidatorTests
{
    [Theory]
    [InlineData("Files/QrCode1.png")]
    [InlineData("Files/QrCode2.png")]
    public void DecodeCommand_Should_Be_Valid(string input)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new DecodeCommand(qrCodeService, consoleService)
        {
            Input = input
        };
        var validator = new DecodeCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeTrue();
    }
}