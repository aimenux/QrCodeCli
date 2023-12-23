[![.NET](https://github.com/aimenux/QrCodeCli/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/aimenux/QrCodeCli/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/QrCodeCli)](https://www.nuget.org/packages/CertificateCli/)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=QrCodeCli-Key&metric=coverage)](https://sonarcloud.io/summary/new_code?id=QrCodeCli-Key)

# QrCodeCli
```
A global tool to encode and decode qr codes
```

> In this repo, i m building a global tool that allows to encode and decode qr codes.
>
> The tool is based on two sub commands :
> - Use sub command `Encode` to encode qr codes
> - Use sub command `Encode Url` to encode url qr codes
> - Use sub command `Encode Sms` to encode sms qr codes
> - Use sub command `Encode Mail` to encode mail qr codes
> - Use sub command `Encode Sepa` to encode sepa qr codes
> - Use sub command `Encode Wifi` to encode wifi qr codes
> - Use sub command `Encode Location` to encode location qr codes
> - Use sub command `Decode` to decode qr codes
>
>
> To run the tool, type commands :
> - `QrCode -h` to show help
> - `QrCode -v` to show version
> - `QrCode -s` to show settings
> - `QrCode Encode -i [input-text]` to encode qr code from plain text
> - `QrCode Encode -i [input-file]` to encode qr code from text file
> - `QrCode Decode -i [input-file]` to decode qr code from image file
>
>
> To install global tool from a local source path, type commands :
> - `dotnet tool install -g --configfile .\nugets\local.config QrCodeCli --version "*-*" --ignore-failed-sources`
>
> To install global tool from [nuget source](https://www.nuget.org/packages/QrCodeCli), type these command :
> - For stable version : `dotnet tool install -g QrCodeCli --ignore-failed-sources`
> - For prerelease version : `dotnet tool install -g QrCodeCli --version "*-*" --ignore-failed-sources`
>
> To uninstall global tool, type these command :
> - `dotnet tool uninstall -g QrCodeCli`
>
>

**`Tools`** : vs22, net 6.0/7.0, qrcoder, zxing, command-line, spectre-console