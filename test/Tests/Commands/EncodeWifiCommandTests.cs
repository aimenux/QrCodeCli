using App.Commands;
using App.Configuration;
using App.Services.QrCode;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Tests.Commands.Fakes;

namespace Tests.Commands;

public class EncodeWifiCommandTests
{
    [Theory]
    [InlineData("id", "pass", "wpa")]
    [InlineData("id", "pass", "wep")]
    public void Should_Encode_QrCode(string ssid, string password, string mode)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeWifiCommand(qrCodeService, consoleService)
        {
            Ssid = ssid,
            Password = password,
            Mode = mode
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ok);
    }
    
    [Theory]
    [InlineData("id", "", "wpa")]
    [InlineData("", "pass", "wpa")]
    [InlineData("id", "pass", "")]
    [InlineData("id", "pass", "what")]
    public void Should_Not_Encode_QrCode(string ssid, string password, string mode)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeWifiCommand(qrCodeService, consoleService)
        {
            Ssid = ssid,
            Password = password,
            Mode = mode
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ko);
    }
}