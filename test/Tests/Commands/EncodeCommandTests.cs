using App.Commands;
using App.Configuration;
using App.Services.QrCode;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Tests.Commands.Fakes;

namespace Tests.Commands;

public class EncodeCommandTests
{
    [Theory]
    [InlineData(10, "L", "Hello QrCode")]
    [InlineData(10, "M", "Hello QrCode")]
    [InlineData(10, "Q", "Hello QrCode")]
    [InlineData(10, "H", "Hello QrCode")]
    public void Should_Encode_QrCode(int size, string level, string input)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeCommand(qrCodeService, consoleService)
        {
            Size = size,
            Level = level,
            Input = input
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ok);
    }
    
    [Theory]
    [InlineData(10, "L", "")]
    [InlineData(10, "L", null)]
    [InlineData(0, "M", "Hello QrCode")]
    [InlineData(-1, "M", "Hello QrCode")]
    [InlineData(10, "A", "Hello QrCode")]
    public void Should_Not_Encode_QrCode(int size, string level, string input)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeCommand(qrCodeService, consoleService)
        {
            Size = size,
            Level = level,
            Input = input
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ko);
    }
}