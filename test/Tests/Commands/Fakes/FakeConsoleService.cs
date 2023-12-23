﻿using App.Services.Console;
using App.Services.QrCode;
using App.Validators;
using Spectre.Console;

namespace Tests.Commands.Fakes;

public class FakeConsoleService : IConsoleService
{
    public void RenderTitle(string text)
    {
    }

    public void RenderVersion(string version)
    {
    }

    public void RenderProblem(string text)
    {
    }

    public void RenderText(string text, Color color)
    {
    }

    public void RenderSettingsFile(string filepath)
    {
    }

    public void RenderUserSecretsFile(string filepath)
    {
    }

    public void RenderException(Exception exception)
    {
    }

    public void RenderStatus(Action action)
    {
        action?.Invoke();
    }

    public bool GetYesOrNoAnswer(string text, bool defaultAnswer)
    {
        return defaultAnswer;
    }

    public void RenderValidationErrors(ValidationErrors validationErrors)
    {
    }

    public void RenderDecodedQrCode(string qrCode, QrCodeParameters parameters)
    {
    }

    public void RenderEncodedQrCode(QrCodeParameters parameters)
    {
    }
}