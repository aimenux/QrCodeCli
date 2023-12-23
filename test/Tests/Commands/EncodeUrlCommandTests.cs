using App.Commands;
using App.Configuration;
using App.Services.QrCode;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Tests.Commands.Fakes;

namespace Tests.Commands;

public class EncodeUrlCommandTests
{
    [Theory]
    [InlineData("https://www.google.com")]
    [InlineData("https://www.amazon.com")]
    public void Should_Encode_QrCode(string url)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeUrlCommand(qrCodeService, consoleService)
        {
            Url = url
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ok);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("null")]
    public void Should_Not_Encode_QrCode(string url)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeUrlCommand(qrCodeService, consoleService)
        {
            Url = url
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ko);
    }
}