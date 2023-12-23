﻿using System.Diagnostics.CodeAnalysis;
using App.Commands;
using App.Configuration;
using App.Extensions;
using App.Services.Console;
using App.Services.QrCode;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            return await CreateHostBuilder(args).RunCommandLineApplicationAsync<ToolCommand>(args);
        }
        catch (Exception ex)
        {
            ConsoleService.RenderAnyException(ex);
            return Settings.ExitCode.Ko;
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, config) =>
            {
                config.AddJsonFile();
                config.AddUserSecrets();
                config.AddEnvironmentVariables();
                config.AddCommandLine(args);
            })
            .ConfigureLogging((_, loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddDefaultLogger();
            })
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<ToolCommand>();
                services.AddTransient<IQrCodeService, QrCodeService>();
                services.AddTransient<IConsoleService, ConsoleService>();
            })
            .AddSerilog();
}