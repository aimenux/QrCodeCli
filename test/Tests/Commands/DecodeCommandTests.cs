using App.Commands;
using App.Configuration;
using App.Services.QrCode;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Tests.Commands.Fakes;

namespace Tests.Commands;

public class DecodeCommandTests
{
    [Theory]
    [InlineData("Files/QrCode1.png")]
    [InlineData("Files/QrCode2.png")]
    public void Should_Decode_QrCode(string input)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new DecodeCommand(qrCodeService, consoleService)
        {
            Input = input
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ok);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Files/QrCode.png")]
    public void Should_Not_Decode_QrCode(string input)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new DecodeCommand(qrCodeService, consoleService)
        {
            Input = input
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ko);
    }
}