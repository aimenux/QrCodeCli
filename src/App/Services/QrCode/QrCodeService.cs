using App.Extensions;
using QRCoder;
using SkiaSharp;
using ZXing.SkiaSharp;

namespace App.Services.QrCode;

public class QrCodeService : IQrCodeService
{
    public void EncodeQrCode(QrCodeParameters parameters)
    {
        var qrCodeText = parameters.QrCodeText();
        using var qrCodeGenerator = new QRCodeGenerator();
        using var qrCodeData = qrCodeGenerator.CreateQrCode(qrCodeText, parameters.Level);
        using var qrCodeBitmap = new BitmapByteQRCode(qrCodeData);
        var qrCodeBytes = qrCodeBitmap.GetGraphic(parameters.Size);
        File.WriteAllBytes(parameters.OutputFile, qrCodeBytes);
    }

    public string DecodeQrCode(QrCodeParameters parameters)
    {
        var reader = new BarcodeReader();
        using var bitmap = SKBitmap.Decode(parameters.Input);
        var result = reader.Decode(bitmap);
        return result?.Text.TrimLineBreaks();
    }
}