using App.Extensions;
using App.Services.Console;
using App.Services.QrCode;
using McMaster.Extensions.CommandLineUtils;
using static App.Configuration.Settings;

namespace App.Commands;

[Command("Encode", FullName = "Encode qr code(s)", Description = "Encode qr code(s).")]
[Subcommand(typeof(EncodeUrlCommand), typeof(EncodeSmsCommand), typeof(EncodeMailCommand), typeof(EncodeSepaCommand), typeof(EncodeWifiCommand), typeof(EncodeLocationCommand))]
public class EncodeCommand : AbstractCommand
{
    private readonly IQrCodeService _qrCodeService;
    
    public EncodeCommand(IQrCodeService qrCodeService, IConsoleService consoleService) : base(consoleService)
    {
        _qrCodeService = qrCodeService ?? throw new ArgumentNullException(nameof(qrCodeService));
    }
    
    [Option("-i|--input", "Input text or file", CommandOptionType.SingleValue)]
    public string Input { get; init; }

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
            var parameters = new QrCodeParameters
            {
                Size = Size,
                Input = Input,
                OutputFile = outputFile,
                Type = QrCodeTypes.PlainText,
                Level = Enum.Parse<EccLevel>(Level, true)
            };
            
            _qrCodeService.EncodeQrCode(parameters);
            ConsoleService.RenderEncodedQrCode(parameters);
        });
    }
}