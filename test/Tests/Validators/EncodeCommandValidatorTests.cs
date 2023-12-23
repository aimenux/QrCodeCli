using App.Commands;
using App.Services.Console;
using App.Services.QrCode;
using App.Validators;
using FluentAssertions;
using NSubstitute;

namespace Tests.Validators;

public class EncodeCommandValidatorTests
{
    [Theory]
    [InlineData(10, "L", "Hello QrCode")]
    [InlineData(10, "M", "Hello QrCode")]
    [InlineData(10, "Q", "Hello QrCode")]
    [InlineData(10, "H", "Hello QrCode")]
    public void EncodeCommand_Should_Be_Valid(int size, string level, string input)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeCommand(qrCodeService, consoleService)
        {
            Size = size,
            Level = level,
            Input = input
        };
        var validator = new EncodeCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(10, "L", "")]
    [InlineData(10, "M", null)]
    [InlineData(0, "Q", "Hello QrCode")]
    [InlineData(-1, "H", "Hello QrCode")]
    [InlineData(10, "A", "Hello QrCode")]
    public void EncodeCommand_Should_Not_Be_Valid(int size, string level, string input)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeCommand(qrCodeService, consoleService)
        {
            Size = size,
            Level = level,
            Input = input
        };
        var validator = new EncodeCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeFalse();
    }
}