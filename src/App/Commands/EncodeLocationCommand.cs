using App.Extensions;
using App.Services.Console;
using App.Services.QrCode;
using McMaster.Extensions.CommandLineUtils;
using static App.Configuration.Settings;

namespace App.Commands;

[Command("Location", FullName = "Encode location qr code(s)", Description = "Encode location qr code(s).")]
[Subcommand(typeof(EncodeUrlCommand), typeof(EncodeSmsCommand), typeof(EncodeSepaCommand), typeof(EncodeWifiCommand))]
public class EncodeLocationCommand : AbstractCommand
{
    private readonly IQrCodeService _qrCodeService;
    
    public EncodeLocationCommand(IQrCodeService qrCodeService, IConsoleService consoleService) : base(consoleService)
    {
        _qrCodeService = qrCodeService ?? throw new ArgumentNullException(nameof(qrCodeService));
    }
    
    [Option("--latitude", "Latitude with '.' as splitter", CommandOptionType.SingleValue)]
    public string Latitude { get; init; }
    
    [Option("--longitude", "Longitude with '.' as splitter", CommandOptionType.SingleValue)]
    public string Longitude { get; init; }

    [Option("--encoding", "Encoding type", CommandOptionType.SingleValue)]
    public string Encoding { get; init; } = $"{DefaultLocationEncoding}";

    [Option("-s|--size", "Pixel size", CommandOptionType.SingleValue)]
    public int Size { get; init; } = DefaultSize;

    [Option("-l|--ecc-level", "Ecc level", CommandOptionType.SingleValue)]
    public string Level { get; init; } = $"{DefaultLevel}";
    
    [Option("-o|--output", "OutputDirectory", CommandOptionType.SingleValue)]
    public string OutputDirectory { get; init; } = GetDefaultWorkingDirectory();

    protected override void Execute(CommandLineApplication app)
    {
        ConsoleService.RenderStatus(() =>
        {
            var outputFile = OutputDirectory.GenerateFileName();
            var input = QrCodeFormatsBuilder.Location(Latitude, Longitude, Encoding);
            var parameters = new QrCodeParameters
            {
                Size = Size,
                Input = input,
                OutputFile = outputFile,
                Type = QrCodeTypes.Location,
                Level = Enum.Parse<EccLevel>(Level, true)
            };
            
            _qrCodeService.EncodeQrCode(parameters);
            ConsoleService.RenderEncodedQrCode(parameters);
        });
    }
}