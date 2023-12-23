using App.Commands;
using App.Services.Console;
using App.Services.QrCode;
using App.Validators;
using FluentAssertions;
using NSubstitute;

namespace Tests.Validators;

public class EncodeWifiCommandValidatorTests
{
    [Theory]
    [InlineData("id", "pass", "wpa")]
    [InlineData("id", "pass", "wep")]
    public void EncodeWifiCommand_Should_Be_Valid(string ssid, string password, string mode)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeWifiCommand(qrCodeService, consoleService)
        {
            Ssid = ssid,
            Password = password,
            Mode = mode
        };
        var validator = new EncodeWifiCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("id", "", "wpa")]
    [InlineData("", "pass", "wpa")]
    [InlineData("id", "pass", "")]
    [InlineData("id", "pass", "what")]
    public void EncodeWifiCommand_Should_Not_Be_Valid(string ssid, string password, string mode)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeWifiCommand(qrCodeService, consoleService)
        {
            Ssid = ssid,
            Password = password,
            Mode = mode
        };
        var validator = new EncodeWifiCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeFalse();
    }
}