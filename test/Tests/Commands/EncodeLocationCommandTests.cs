using App.Commands;
using App.Configuration;
using App.Services.QrCode;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Tests.Commands.Fakes;

namespace Tests.Commands;

public class EncodeLocationCommandTests
{
    [Theory]
    [InlineData("51.26118", "6.6717", "Geo")]
    [InlineData("51.26118", "6.6717", "GoogleMaps")]
    public void Should_Encode_QrCode(string latitude, string longitude, string encoding)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeLocationCommand(qrCodeService, consoleService)
        {
            Latitude = latitude,
            Longitude = longitude,
            Encoding = encoding
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ok);
    }
    
    [Theory]
    [InlineData("", "6.6717", "Geo")]
    [InlineData("51.26118", "", "Geo")]
    [InlineData("51.26118", "6.6717", "")]
    [InlineData("51.26118", "6.6717", "G")]
    [InlineData("A1.26118", "6.6717", "Geo")]
    [InlineData("51.26118", "B.6717", "Geo")]
    public void Should_Not_Encode_QrCode(string latitude, string longitude, string encoding)
    {
        // arrange
        var app = new CommandLineApplication();
        var qrCodeService = new QrCodeService();
        var consoleService = new FakeConsoleService();
        var command = new EncodeLocationCommand(qrCodeService, consoleService)
        {
            Latitude = latitude,
            Longitude = longitude,
            Encoding = encoding
        };
        
        // act
        var result = command.OnExecute(app);

        // assert
        result.Should().Be(Settings.ExitCode.Ko);
    }
}