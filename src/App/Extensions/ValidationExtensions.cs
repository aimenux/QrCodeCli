using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace App.Extensions;

[ExcludeFromCodeCoverage]
public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, string> UrlIsValid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must<T, string>((Func<T, string, bool>) ((x, val) => val.IsValidUrl()))
            .WithMessage("Url '{PropertyValue}' is not valid.");
    }
    
    public static IRuleBuilderOptions<T, string> CoordinateIsValid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must<T, string>((Func<T, string, bool>) ((x, val) => val.IsValidCoordinate()))
            .WithMessage("Coordinate '{PropertyValue}' is not valid.");
    }
    
    public static IRuleBuilderOptions<T, string> AmountIsValid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must<T, string>((Func<T, string, bool>) ((x, val) => val.IsDecimal(out _)))
            .WithMessage("Amount '{PropertyValue}' is not valid.");
    }
    
    public static IRuleBuilderOptions<T, string> AmountIsBetween<T>(this IRuleBuilder<T, string> ruleBuilder, decimal min, decimal max)
    {
        return ruleBuilder
            .Must<T, string>((Func<T, string, bool>) ((x, val) => val.IsDecimal(out var amount) && amount >= min && amount <= max))
            .WithMessage("Amount '{PropertyValue}' is not between '{min}' and '{max}'.");
    }
    
    public static IRuleBuilderOptions<T, string> FileExists<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must<T, string>((Func<T, string, bool>) ((x, val) => File.Exists(val)))
            .WithMessage("File '{PropertyValue}' does not exist.");
    }
    
    public static IRuleBuilderOptions<T, string> FileNotEmpty<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must<T, string>((Func<T, string, bool>) ((x, val) => new FileInfo(val).Length > 0))
            .WithMessage("File '{PropertyValue}' is empty.");
    }
}