using App.Commands;
using App.Extensions;
using FluentValidation;

namespace App.Validators;

public class EncodeSepaCommandValidator : AbstractValidator<EncodeSepaCommand>
{
    public EncodeSepaCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Iban)
            .NotEmpty();
        
        RuleFor(x => x.Bic)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Amount)
            .NotEmpty()
            .AmountIsValid()
            .AmountIsBetween(0.01m, 999999999.99m);
        
        RuleFor(x => x.Type)
            .NotEmpty()
            .IsEnumName(typeof(RemittanceType), caseSensitive: false);
        
        RuleFor(x => x.Version)
            .NotEmpty()
            .IsEnumName(typeof(GiroCodeVersion), caseSensitive: false);
        
        RuleFor(x => x.Encoding)
            .NotEmpty()
            .IsEnumName(typeof(GiroCodeEncoding), caseSensitive: false);
        
        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.Level)
            .NotEmpty()
            .IsEnumName(typeof(EccLevel), caseSensitive: false);
    }
}