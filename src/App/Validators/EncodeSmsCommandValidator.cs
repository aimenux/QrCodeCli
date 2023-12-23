using App.Commands;
using FluentValidation;

namespace App.Validators;

public class EncodeSmsCommandValidator : AbstractValidator<EncodeSmsCommand>
{
    public EncodeSmsCommandValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty();
        
        RuleFor(x => x.Subject)
            .NotEmpty();

        RuleFor(x => x.Encoding)
            .NotEmpty()
            .IsEnumName(typeof(SmsEncoding), caseSensitive: false);
        
        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.Level)
            .NotEmpty()
            .IsEnumName(typeof(EccLevel), caseSensitive: false);
    }
}