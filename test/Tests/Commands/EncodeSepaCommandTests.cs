using App.Commands;
using App.Configuration;
using App.Services.QrCode;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Tests.Commands.Fakes;

namespace Tests.Commands;

public class EncodeSepaCommandTests
{
    [Theory]
    [InlineData("DE33100205000001194700", "BFSWDE33BER", "Xyz", "10,23")]
    [InlineData("DE33100205000001194700", "BFSWDE33BER", "Xyz", "10.23")]
    public void Should_Encode_QrCode(string iban, string bic, string name, string amount)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeSepaCommand(qrCodeService, consoleService)
        {
            Iban = iban,
            Bic = bic,
            Name = name,
            Amount = amount
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ok);
    }
    
    [Theory]
    [InlineData("", "BFS33BER", "Xyz", "10,23")]
    [InlineData("DE33100205000001194700", "", "Xyz", "10,23")]
    [InlineData("DE33100205000001194700", "BFS33BER", "", "10,23")]
    [InlineData("DE33100205000001194700", "BFS33BER", "Xyz", "")]
    [InlineData("DE33100205000001194700", "BFS33BER", "Xyz", "0")]
    [InlineData("DE33100205000001194700", "BFS33BER", "Xyz", "ABC")]
    [InlineData("DE33100205000001194700", "BFS33BER", "Xyz", "-10")]
    public void Should_Not_Encode_QrCode(string iban, string bic, string name, string amount)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeSepaCommand(qrCodeService, consoleService)
        {
            Iban = iban,
            Bic = bic,
            Name = name,
            Amount = amount
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ko);
    }
}