namespace App.Services.QrCode;

public class QrCodeParameters
{
    public int Size { get; init; }
    
    public EccLevel Level { get; init; }
    
    public string Input { get; init; }
    
    public string OutputFile { get; init; }
    
    public QrCodeTypes Type { get; init; }

    public string QrCodeText()
    {
        return File.Exists(Input)
            ? File.ReadAllText(Input)
            : Input;
    }
}