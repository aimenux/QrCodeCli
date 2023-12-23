using App.Commands;
using App.Extensions;
using FluentValidation;

namespace App.Validators;

public class DecodeCommandValidator : AbstractValidator<DecodeCommand>
{
    public DecodeCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Input)
            .NotEmpty()
            .FileExists()
            .FileNotEmpty();
    }
}