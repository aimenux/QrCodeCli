using App.Extensions;
using App.Services.Console;
using App.Services.QrCode;
using McMaster.Extensions.CommandLineUtils;
using static App.Configuration.Settings;

namespace App.Commands;

[Command("Mail", FullName = "Encode mail qr code(s)", Description = "Encode mail qr code(s).")]
[Subcommand(typeof(EncodeUrlCommand), typeof(EncodeSmsCommand), typeof(EncodeSepaCommand), typeof(EncodeWifiCommand), typeof(EncodeLocationCommand))]
public class EncodeMailCommand : AbstractCommand
{
    private readonly IQrCodeService _qrCodeService;
    
    public EncodeMailCommand(IQrCodeService qrCodeService, IConsoleService consoleService) : base(consoleService)
    {
        _qrCodeService = qrCodeService ?? throw new ArgumentNullException(nameof(qrCodeService));
    }
    
    [Option("--email", "Receiver's email address", CommandOptionType.SingleValue)]
    public string Email { get; init; }
    
    [Option("--subject", "Email subject", CommandOptionType.SingleValue)]
    public string Subject { get; init; }
    
    [Option("--message", "Email message", CommandOptionType.SingleValue)]
    public string Message { get; init; }

    [Option("--encoding", "Encoding type", CommandOptionType.SingleValue)]
    public string Encoding { get; init; } = $"{DefaultMailEncoding}";

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
            var input = QrCodeFormatsBuilder.Mail(Email, Subject, Message, Encoding);
            var parameters = new QrCodeParameters
            {
                Size = Size,
                Input = input,
                OutputFile = outputFile,
                Type = QrCodeTypes.Mail,
                Level = Enum.Parse<EccLevel>(Level, true)
            };
            
            _qrCodeService.EncodeQrCode(parameters);
            ConsoleService.RenderEncodedQrCode(parameters);
        });
    }
}