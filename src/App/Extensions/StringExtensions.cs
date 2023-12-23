namespace App.Extensions;

public static class StringExtensions
{
    public static bool IgnoreEquals(this string left, string right)
    {
        return string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
    }
    
    public static bool IsValidUrl(this string input)
    {
        return Uri.IsWellFormedUriString(input, UriKind.Absolute);
    }
    
    public static string TrimLineBreaks(this string input)
    {
        return input?.Trim('\r', '\n');
    }
    
    public static bool IsValidCoordinate(this string input)
    {
        return !string.IsNullOrWhiteSpace(input) && input.All(c => char.IsDigit(c) || c == '.');
    }
}