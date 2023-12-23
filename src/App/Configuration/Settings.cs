using System.Reflection;
using App.Commands;

namespace App.Configuration;

public sealed class Settings
{
    public static class Cli
    {
        public const string UsageName = @"QrCodeCli";
        public const string FriendlyName = @"QrCodeCli";
        public const string Description = @"A net global tool helping to encode and decode qr code(s).";
        public static readonly string UserSecretsFile = $@"C:\Users\{Environment.UserName}\AppData\Roaming\Microsoft\UserSecrets\{UsageName}-UserSecrets\secrets.json";
        public static readonly string Version = GetInformationalVersion().Split("+").FirstOrDefault();
        
        private static string GetInformationalVersion()
        {
            return typeof(ToolCommand)
                .Assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                .InformationalVersion;
        }
    }
    
    public static class ExitCode
    {
        public const int Ok = 0;
        public const int Ko = -1;
    }
    
    public static string GetDefaultWorkingDirectory()
    {
        const string defaultDirectory = @"C:\Logs";
        var directory = Directory.Exists(defaultDirectory) 
            ? defaultDirectory 
            : "./";
        return Path.GetFullPath(directory);
    }

    public const int DefaultSize = 20;
    public const EccLevel DefaultLevel = EccLevel.H;
    public const GiroCodeVersion DefaultVersion = GiroCodeVersion.Version2;
    public const SmsEncoding DefaultSmsEncoding = SmsEncoding.SMSTO;
    public const MailEncoding DefaultMailEncoding = MailEncoding.MAILTO;
    public const GiroCodeEncoding DefaultSepaEncoding = GiroCodeEncoding.ISO_8859_1;
    public const RemittanceType DefaultType = RemittanceType.Unstructured;
    public const GeolocationEncoding DefaultLocationEncoding = GeolocationEncoding.GEO;
}