using System.Diagnostics.CodeAnalysis;
using System.Web;
using Spectre.Console;

namespace App.Extensions;

[ExcludeFromCodeCoverage]
public static class SpectreExtensions
{
    public static Markup ToMarkupLink(this string text)
    {
        var link = $"[green][link={UrlEncode(text)}]{text}[/][/]";
        return link.ToMarkup();
    }
    
    public static Markup ToMarkup(this string text)
    {
        try
        {
            return new Markup(text ?? string.Empty);
        }
        catch
        {
            return ErrorMarkup;
        }
    }

    private static readonly Markup ErrorMarkup = new(Emoji.Known.CrossMark);
    
    private static string UrlEncode(string url)
    {
        return url.Contains(' ') ? HttpUtility.UrlEncode(url) : url;
    }
}