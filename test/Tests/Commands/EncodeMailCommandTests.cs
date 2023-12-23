using App.Commands;
using App.Configuration;
using App.Services.QrCode;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Tests.Commands.Fakes;

namespace Tests.Commands;

public class EncodeMailCommandTests
{
    [Theory]
    [InlineData("xyz@gmail.com", "abc", "123", "SMTP")]
    [InlineData("xyz@gmail.com", "abc", "123", "MATMSG")]
    [InlineData("xyz@gmail.com", "abc", "123", "MAILTO")]
    public void Should_Encode_QrCode(string email, string subject, string message, string encoding)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeMailCommand(qrCodeService, consoleService)
        {
            Email = email,
            Subject = subject,
            Message = message,
            Encoding = encoding
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ok);
    }
    
    [Theory]
    [InlineData("", "abc", "123", "MAILTO")]
    [InlineData(null, "abc", "123", "MAILTO")]
    [InlineData("null", "abc", "123", "MAILTO")]
    [InlineData("xyz.com", "abc", "123", "MAILTO")]
    [InlineData("xyz@gmail.com", "abc", "123", "XYZ")]
    public void Should_Not_Encode_QrCode(string email, string subject, string message, string encoding)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeMailCommand(qrCodeService, consoleService)
        {
            Email = email,
            Subject = subject,
            Message = message,
            Encoding = encoding
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ko);
    }
}