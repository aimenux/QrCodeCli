using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace App.Extensions;

[ExcludeFromCodeCoverage]
public static class PathExtensions
{
    public static string GetSettingFilePath() => Path.GetFullPath(Path.Combine(GetDirectoryPath(), @"appsettings.json"));

    public static string GenerateFileName(this string path, string prefix = "QrCode", string extension = ".png")
    {
        return Path.Combine(path, $"{prefix}-{DateTime.Now:yyMMddHHmmss}{extension}");
    }

    public static string GetDirectoryPath()
    {
        try
        {
            return Path.GetDirectoryName(GetAssemblyLocation());
        }
        catch
        {
            return Directory.GetCurrentDirectory();
        }
    }

    public static string GetAssemblyLocation() => Assembly.GetExecutingAssembly().Location;

    public static bool IsMaybeFile(this string input)
    {
        return !string.IsNullOrWhiteSpace(input)
               && Path.IsPathRooted(input)
               && Path.HasExtension(input);
    }
}