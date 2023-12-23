using App.Commands;
using App.Extensions;
using FluentValidation;

namespace App.Validators;

public class EncodeCommandValidator : AbstractValidator<EncodeCommand>
{
    public EncodeCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Input)
            .NotEmpty();

        When(x => x.Input.IsMaybeFile(), () =>
        {
            RuleFor(x => x.Input)
                .NotEmpty()
                .FileExists()
                .FileNotEmpty();
        });
        
        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.Level)
            .NotEmpty()
            .IsEnumName(typeof(EccLevel), caseSensitive: false);
    }
}