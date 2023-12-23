using App.Commands;
using App.Extensions;
using FluentValidation;

namespace App.Validators;

public class EncodeMailCommandValidator : AbstractValidator<EncodeMailCommand>
{
    public EncodeMailCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(x => x.Encoding)
            .NotEmpty()
            .IsEnumName(typeof(MailEncoding), caseSensitive: false);
        
        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.Level)
            .NotEmpty()
            .IsEnumName(typeof(EccLevel), caseSensitive: false);
    }
}