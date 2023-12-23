using App.Commands;
using FluentValidation;

namespace App.Validators;

public class EncodeWifiCommandValidator : AbstractValidator<EncodeWifiCommand>
{
    public EncodeWifiCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Ssid)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.Mode)
            .NotEmpty()
            .IsEnumName(typeof(AuthenticationMode), caseSensitive: false);
        
        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.Level)
            .NotEmpty()
            .IsEnumName(typeof(EccLevel), caseSensitive: false);
    }
}