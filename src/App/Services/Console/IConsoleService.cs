using App.Services.QrCode;
using App.Validators;
using Spectre.Console;

namespace App.Services.Console;

public interface IConsoleService
{
    void RenderTitle(string text);
    void RenderVersion(string version);
    void RenderProblem(string text);
    void RenderText(string text, Color color);
    void RenderSettingsFile(string filepath);
    void RenderUserSecretsFile(string filepath);
    void RenderException(Exception exception);
    void RenderStatus(Action action);
    bool GetYesOrNoAnswer(string text, bool defaultAnswer);
    void RenderValidationErrors(ValidationErrors validationErrors);
    void RenderDecodedQrCode(string qrCode, QrCodeParameters parameters);
    void RenderEncodedQrCode(QrCodeParameters parameters);
}