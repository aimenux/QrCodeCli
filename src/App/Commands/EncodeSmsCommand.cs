using App.Extensions;
using App.Services.Console;
using App.Services.QrCode;
using McMaster.Extensions.CommandLineUtils;
using static App.Configuration.Settings;

namespace App.Commands;

[Command("Sms", FullName = "Encode sms qr code(s)", Description = "Encode sms qr code(s).")]
[Subcommand(typeof(EncodeSepaCommand), typeof(EncodeWifiCommand))]
public class EncodeSmsCommand : AbstractCommand
{
    private readonly IQrCodeService _qrCodeService;
    
    public EncodeSmsCommand(IQrCodeService qrCodeService, IConsoleService consoleService) : base(consoleService)
    {
        _qrCodeService = qrCodeService ?? throw new ArgumentNullException(nameof(qrCodeService));
    }
    
    [Option("--number", "Receiver phone number", CommandOptionType.SingleValue)]
    public string Number { get; init; }
    
    [Option("--subject", "Text to send", CommandOptionType.SingleValue)]
    public string Subject { get; init; }

    [Option("--encoding", "Encoding type", CommandOptionType.SingleValue)]
    public string Encoding { get; init; } = $"{DefaultSmsEncoding}";

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
            var input = QrCodeFormatsBuilder.Sms(Number, Subject, Encoding);
            var parameters = new QrCodeParameters
            {
                Size = Size,
                Input = input,
                OutputFile = outputFile,
                Type = QrCodeTypes.Sms,
                Level = Enum.Parse<EccLevel>(Level, true)
            };
            
            _qrCodeService.EncodeQrCode(parameters);
            ConsoleService.RenderEncodedQrCode(parameters);
        });
    }
}