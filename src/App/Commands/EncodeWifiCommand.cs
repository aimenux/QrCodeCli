using App.Extensions;
using App.Services.Console;
using App.Services.QrCode;
using McMaster.Extensions.CommandLineUtils;
using static App.Configuration.Settings;

namespace App.Commands;

[Command("Wifi", FullName = "Encode wifi qr code(s)", Description = "Encode wifi qr code(s).")]
public class EncodeWifiCommand : AbstractCommand
{
    private readonly IQrCodeService _qrCodeService;
    
    public EncodeWifiCommand(IQrCodeService qrCodeService, IConsoleService consoleService) : base(consoleService)
    {
        _qrCodeService = qrCodeService ?? throw new ArgumentNullException(nameof(qrCodeService));
    }
    
    [Option("--ssid", "Ssid of the wifi network", CommandOptionType.SingleValue)]
    public string Ssid { get; init; }

    [Option("--pass", "Password of the wifi network", CommandOptionType.SingleValue)]
    public string Password { get; init; }

    [Option("--mode", "Authentication mode", CommandOptionType.SingleValue)]
    public string Mode { get; init; }
    
    [Option("--hide", "Hide ssid", CommandOptionType.NoValue)]
    public bool HideSsid { get; init; }

    [Option("--escape", "Escape hex", CommandOptionType.NoValue)]
    public bool EscapeHex { get; init; } = true;
    
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
            var input = QrCodeFormatsBuilder.Wifi(Ssid, Password, Mode, HideSsid, EscapeHex);
            var parameters = new QrCodeParameters
            {
                Size = Size,
                Input = input,
                OutputFile = outputFile,
                Type = QrCodeTypes.Wifi,
                Level = Enum.Parse<EccLevel>(Level, true)
            };
            
            _qrCodeService.EncodeQrCode(parameters);
            ConsoleService.RenderEncodedQrCode(parameters);
        });
    }
}