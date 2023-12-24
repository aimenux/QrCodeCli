using App.Extensions;
using QRCoder;

namespace App.Services.QrCode;

public static class QrCodeFormatsBuilder
{
    public static string Sepa(
        string iban,
        string bic,
        string name,
        string amount,
        string information,
        string type,
        string purpose,
        string message,
        string version,
        string encoding
    )
    {
        var sepaVersion = Enum.Parse<GiroCodeVersion>(version, true);
        var sepaEncoding = Enum.Parse<GiroCodeEncoding>(encoding, true);
        var remittanceType = Enum.Parse<RemittanceType>(type, true);
        var sepa = new PayloadGenerator.Girocode(
            iban,
            bic,
            name,
            amount.ToDecimal(), 
            information,
            remittanceType,
            purpose,
            message,
            sepaVersion,
            sepaEncoding
            );
        return sepa.ToString();
    }
    
    public static string Url(string link)
    {
        var url = new PayloadGenerator.Url(link);
        return url.ToString();
    }    
    
    public static string Mail(
        string email,
        string subject,
        string message,
        string encoding
    )
    {
        var encodingType = Enum.Parse<MailEncoding>(encoding, true);
        var mail = new PayloadGenerator.Mail(
            email,
            subject,
            message,
            encodingType);
        return mail.ToString();
    }   
    
    public static string Sms(
        string number,
        string subject,
        string encoding
    )
    {
        var encodingType = Enum.Parse<SmsEncoding>(encoding, true);
        var sms = new PayloadGenerator.SMS(
            number,
            subject,
            encodingType);
        return sms.ToString();
    }    
    
    public static string Wifi(
        string ssid,
        string password,
        string mode,
        bool hideSsid,
        bool escapeHex
        )
    {
        var authenticationMode = Enum.Parse<AuthenticationMode>(mode, true);
        var wifi = new PayloadGenerator.WiFi(
            ssid,
            password,
            authenticationMode,
            hideSsid,
            escapeHex);
        return wifi.ToString();
    }
    
    public static string Location(string latitude, string longitude, string encoding)
    {
        var locationEncoding = Enum.Parse<GeolocationEncoding>(encoding, true);
        var location = new PayloadGenerator.Geolocation(latitude, longitude, locationEncoding);
        return location.ToString();
    }
}