using App.Commands;
using App.Extensions;
using FluentValidation;

namespace App.Validators;

public class EncodeUrlCommandValidator : AbstractValidator<EncodeUrlCommand>
{
    public EncodeUrlCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Url)
            .NotEmpty()
            .UrlIsValid();

        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.Level)
            .NotEmpty()
            .IsEnumName(typeof(EccLevel), caseSensitive: false);
    }
}