namespace App.Services.QrCode;

public interface IQrCodeService
{
    void EncodeQrCode(QrCodeParameters parameters);
    string DecodeQrCode(QrCodeParameters parameters);
}