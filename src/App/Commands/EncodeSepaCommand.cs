using App.Extensions;
using App.Services.Console;
using App.Services.QrCode;
using McMaster.Extensions.CommandLineUtils;
using static App.Configuration.Settings;

namespace App.Commands;

[Command("Sepa", FullName = "Encode sepa qr code(s)", Description = "Encode sepa qr code(s).")]
public class EncodeSepaCommand : AbstractCommand
{
    private readonly IQrCodeService _qrCodeService;
    
    public EncodeSepaCommand(IQrCodeService qrCodeService, IConsoleService consoleService) : base(consoleService)
    {
        _qrCodeService = qrCodeService ?? throw new ArgumentNullException(nameof(qrCodeService));
    }
    
    [Option("--iban", "Iban of the beneficiary", CommandOptionType.SingleValue)]
    public string Iban { get; init; }

    [Option("--bic", "Bic of the beneficiary", CommandOptionType.SingleValue)]
    public string Bic { get; init; }

    [Option("--name", "Name of the beneficiary", CommandOptionType.SingleValue)]
    public string Name { get; init; }
    
    [Option("--amount", "Amount of the bank transfer", CommandOptionType.SingleValue)]
    public string Amount { get; init; }

    [Option("--info", "Remittance information", CommandOptionType.SingleValue)]
    public string Information { get; init; } = string.Empty;
    
    [Option("--type", "Remittance type", CommandOptionType.SingleValue)]
    public string Type { get; init; } = $"{DefaultType}";
    
    [Option("--purpose", "Purpose of the bank Transfer", CommandOptionType.SingleValue)]
    public string Purpose { get; init; } = string.Empty;
    
    [Option("--message", "Message to the beneficiary", CommandOptionType.SingleValue)]
    public string Message { get; init; } = string.Empty;
    
    [Option("--version", "Sepa QrCode version", CommandOptionType.SingleValue)]
    public string Version { get; init; } = $"{DefaultVersion}";
    
    [Option("--encoding", "Sepa QrCode encoding", CommandOptionType.SingleValue)]
    public string Encoding { get; init; } = $"{DefaultSepaEncoding}";
    
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
            var input = QrCodeFormatsBuilder.Sepa(Iban, Bic, Name, Amount, Information, Type, Purpose, Message, Version, Encoding);
            var parameters = new QrCodeParameters
            {
                Size = Size,
                Input = input,
                OutputFile = outputFile,
                Type = QrCodeTypes.Sepa,
                Level = Enum.Parse<EccLevel>(Level, true)
            };
            
            _qrCodeService.EncodeQrCode(parameters);
            ConsoleService.RenderEncodedQrCode(parameters);
        });
    }
}