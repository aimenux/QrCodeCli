using App.Commands;
using App.Services.Console;
using App.Services.QrCode;
using App.Validators;
using FluentAssertions;
using NSubstitute;

namespace Tests.Validators;

public class EncodeLocationCommandValidatorTests
{
    [Theory]
    [InlineData("51.26118", "6.6717", "Geo")]
    [InlineData("51.26118", "6.6717", "GoogleMaps")]
    public void EncodeLocationCommand_Should_Be_Valid(string latitude, string longitude, string encoding)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeLocationCommand(qrCodeService, consoleService)
        {
            Latitude = latitude,
            Longitude = longitude,
            Encoding = encoding
        };
        var validator = new EncodeLocationCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("", "6.6717", "Geo")]
    [InlineData("51.26118", "", "Geo")]
    [InlineData("51.26118", "6.6717", "")]
    [InlineData("51.26118", "6.6717", "G")]
    [InlineData("A1.26118", "6.6717", "Geo")]
    [InlineData("51.26118", "B.6717", "Geo")]
    public void EncodeLocationCommand_Should_Not_Be_Valid(string latitude, string longitude, string encoding)
    {
        // arrange
        var consoleService = Substitute.For<IConsoleService>();
        var qrCodeService = Substitute.For<IQrCodeService>();
        var command = new EncodeLocationCommand(qrCodeService, consoleService)
        {
            Latitude = latitude,
            Longitude = longitude,
            Encoding = encoding
        };
        var validator = new EncodeLocationCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeFalse();
    }
}