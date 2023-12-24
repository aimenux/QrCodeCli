using App.Services.Console;
using App.Services.QrCode;
using McMaster.Extensions.CommandLineUtils;

namespace App.Commands;

[Command("Decode", FullName = "Decode qr code(s)", Description = "Decode qr code(s).")]
public class DecodeCommand : AbstractCommand
{
    private readonly IQrCodeService _qrCodeService;
    
    public DecodeCommand(IQrCodeService qrCodeService, IConsoleService consoleService) : base(consoleService)
    {
        _qrCodeService = qrCodeService ?? throw new ArgumentNullException(nameof(qrCodeService));
    }

    [Option("-i|--input", "Input image", CommandOptionType.SingleValue)]
    public string Input { get; init; }

    protected override void Execute(CommandLineApplication app)
    {
        ConsoleService.RenderStatus(() =>
        {
            var parameters = new QrCodeParameters
            {
                Input = Path.GetFullPath(Input)
            };
            
            var qrCodeText = _qrCodeService.DecodeQrCode(parameters);
            ConsoleService.RenderDecodedQrCode(qrCodeText, parameters);
        });
    }
}