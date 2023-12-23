using App.Extensions;
using App.Services.QrCode;
using FluentAssertions;
using static App.Configuration.Settings;

namespace Tests.Services;

public class QrCodeServiceTests
{
    [Theory]
    [InlineData(10, "L", "Hello QrCode 1")]
    [InlineData(20, "M", "Hello QrCode 2")]
    [InlineData(30, "Q", "Hello QrCode 3")]
    [InlineData(40, "H", "Hello QrCode 4")]
    public void Should_Encode_QrCode(int size, string level, string input)
    {
        // arrange
        var service = new QrCodeService();
        var parameters = new QrCodeParameters
        {
            Size = size,
            Input = input,
            Level = Enum.Parse<EccLevel>(level),
            OutputFile = GetOutputFile()
        };
        
        // act
        service.EncodeQrCode(parameters);

        // assert
        File.Exists(parameters.OutputFile).Should().BeTrue();
    }
    
    [Theory]
    [InlineData("Files/QrCode1.png")]
    [InlineData("Files/QrCode2.png")]
    public void Should_Decode_QrCode(string input)
    {
        // arrange
        var service = new QrCodeService();
        var parameters = new QrCodeParameters
        {
            Input = input
        };
        
        // act
        var qrCode = service.DecodeQrCode(parameters);

        // assert
        qrCode.Should().NotBeNullOrWhiteSpace();
    }

    private static string GetOutputFile() => GetDefaultWorkingDirectory().GenerateFileName();
}