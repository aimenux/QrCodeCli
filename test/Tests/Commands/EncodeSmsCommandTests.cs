using App.Commands;
using App.Configuration;
using App.Services.QrCode;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Tests.Commands.Fakes;

namespace Tests.Commands;

public class EncodeSmsCommandTests
{
    [Theory]
    [InlineData("+491701234567", "Hello", "Sms")]
    [InlineData("+491701234567", "Hello", "SmsTo")]
    [InlineData("+491701234567", "Hello", "Sms_iOS")]
    public void Should_Encode_QrCode(string number, string subject, string encoding)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeSmsCommand(qrCodeService, consoleService)
        {
            Number = number,
            Subject = subject,
            Encoding = encoding
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ok);
    }
    
    [Theory]
    [InlineData("", "Hello", "Sms")]
    [InlineData("+491701234567", "", "Sms")]
    [InlineData("+491701234567", "Hello", "")]
    [InlineData("+491701234567", "Hello", "Mms")]
    public void Should_Not_Encode_QrCode(string number, string subject, string encoding)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeSmsCommand(qrCodeService, consoleService)
        {
            Number = number,
            Subject = subject,
            Encoding = encoding
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ko);
    }
}