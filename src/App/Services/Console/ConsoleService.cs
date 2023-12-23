using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Text.Json;
using App.Configuration;
using App.Extensions;
using App.Services.QrCode;
using App.Validators;
using Spectre.Console;

namespace App.Services.Console;

[ExcludeFromCodeCoverage]
public class ConsoleService : IConsoleService
{
    public ConsoleService()
    {
        System.Console.OutputEncoding = Encoding.UTF8;
    }

    public void RenderTitle(string text)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new FigletText(text));
        AnsiConsole.WriteLine();
    }

    public void RenderVersion(string version)
    {
        var text = $"{Settings.Cli.FriendlyName} V{version}";
        RenderText(text, Color.White);
    }

    public void RenderProblem(string text) => RenderText(text, Color.Red);

    public void RenderText(string text, Color color)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Markup($"[bold {color}]{text}[/]"));
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
    }

    public void RenderSettingsFile(string filepath)
    {
        var name = Path.GetFileName(filepath);
        var json = File.ReadAllText(filepath);
        var formattedJson = GetFormattedJson(json);
        var header = new Rule($"[yellow]({name})[/]");
        header.Centered();
        var footer = new Rule($"[yellow]({filepath})[/]");
        footer.Centered();

        AnsiConsole.WriteLine();
        AnsiConsole.Write(header);
        AnsiConsole.WriteLine(formattedJson);
        AnsiConsole.Write(footer);
        AnsiConsole.WriteLine();
    }
    
    public void RenderUserSecretsFile(string filepath)
    {
        if (!OperatingSystem.IsWindows()) return;
        if (!File.Exists(filepath)) return;
        if (!GetYesOrNoAnswer("display user secrets", false)) return;
        RenderSettingsFile(filepath);
    }

    public void RenderException(Exception exception) => RenderAnyException(exception);

    public static void RenderAnyException<T>(T exception) where T : Exception
    {
        const ExceptionFormats formats = ExceptionFormats.ShortenTypes
                                         | ExceptionFormats.ShortenPaths
                                         | ExceptionFormats.ShortenMethods;

        AnsiConsole.WriteLine();
        AnsiConsole.WriteException(exception, formats);
        AnsiConsole.WriteLine();
    }
    
    public void RenderStatus(Action action)
    {
        var spinner = RandomSpinner();

        AnsiConsole.Status()
            .Start("Work is in progress ...", ctx =>
            {
                ctx.Spinner(spinner);
                action.Invoke();
            });
    }

    public bool GetYesOrNoAnswer(string text, bool defaultAnswer)
    {
        if (AnsiConsole.Confirm($"Do you want to [u]{text}[/] ?", defaultAnswer)) return true;
        AnsiConsole.WriteLine();
        return false;
    }

    public void RenderValidationErrors(ValidationErrors validationErrors)
    {
        var count = validationErrors.Count;

        var table = new Table()
            .BorderColor(Color.White)
            .Border(TableBorder.Square)
            .Title($"[red][bold]{count} error(s)[/][/]")
            .AddColumn(new TableColumn("[u]Name[/]").Centered())
            .AddColumn(new TableColumn("[u]Message[/]").Centered())
            .Caption("[grey][bold]Invalid options/arguments[/][/]");

        foreach (var error in validationErrors)
        {
            var failure = error.Failure;
            var name = $"[bold]{error.OptionName()}[/]";
            var reason = $"[tan]{failure.ErrorMessage}[/]";
            table.AddRow(name.ToMarkup(), reason.ToMarkup());
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    public void RenderDecodedQrCode(string qrCode, QrCodeParameters parameters)
    {
        if (string.IsNullOrWhiteSpace(qrCode))
        {
            RenderText("Failed to decode QrCode.", Color.Red);
            return;
        }
        
        var text = qrCode.ToMarkup();
        var file = parameters.Input.ToMarkupLink();
        
        var table = new Table()
            .BorderColor(Color.White)
            .Border(TableBorder.Square)
            .Title($"[yellow]QrCode successfully decoded[/]")
            .AddColumn(new TableColumn("[u]File[/]").Centered())
            .AddColumn(new TableColumn("[u]Text[/]").Centered())
            .AddRow(file, text);

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    public void RenderEncodedQrCode(QrCodeParameters parameters)
    {
        if (!File.Exists(parameters.OutputFile))
        {
            RenderText("Failed to encode QrCode.", Color.Red);
            return;
        }
        
        var type = $"{parameters.Type}".ToMarkup();
        var level = $"{parameters.Level}".ToMarkup();
        var size = $"{parameters.Size}".ToMarkup();
        var file = parameters.OutputFile.ToMarkupLink();
        
        var table = new Table()
            .BorderColor(Color.White)
            .Border(TableBorder.Square)
            .Title($"[yellow]QrCode successfully encoded[/]")
            .AddColumn(new TableColumn("[u]Type[/]").Centered())
            .AddColumn(new TableColumn("[u]Level[/]").Centered())
            .AddColumn(new TableColumn("[u]Size[/]").Centered())
            .AddColumn(new TableColumn("[u]File[/]").Centered())
            .AddRow(type, level, size, file);

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    private static string GetFormattedJson(string json)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        using var document = JsonDocument.Parse(json);
        return JsonSerializer.Serialize(document, options);
    }

    private static Spinner RandomSpinner() 
    {
        var values = typeof(Spinner.Known)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(x => x.PropertyType == typeof(Spinner))
            .Select(x => (Spinner)x.GetValue(null))
            .ToArray();

        var index = Random.Shared.Next(values.Length);
        var value = (Spinner)values.GetValue(index);
        return value;
    }
}