using App.Commands;
using App.Extensions;
using FluentValidation;

namespace App.Validators;

public class EncodeLocationCommandValidator : AbstractValidator<EncodeLocationCommand>
{
    public EncodeLocationCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Latitude)
            .NotEmpty()
            .CoordinateIsValid();
        
        RuleFor(x => x.Longitude)
            .NotEmpty()
            .CoordinateIsValid();

        RuleFor(x => x.Encoding)
            .NotEmpty()
            .IsEnumName(typeof(GeolocationEncoding), caseSensitive: false);

        RuleFor(x => x.Size)
            .InclusiveBetween(1, 100);

        RuleFor(x => x.Level)
            .NotEmpty()
            .IsEnumName(typeof(EccLevel), caseSensitive: false);
    }
}