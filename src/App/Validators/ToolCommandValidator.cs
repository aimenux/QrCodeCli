using App.Commands;
using App.Exceptions;
using FluentValidation;

namespace App.Validators;

public static class ToolCommandValidator
{
    public static ValidationErrors Validate<TCommand>(TCommand command) where TCommand : AbstractCommand
    {
        return command switch
        {
            ToolCommand _ => ValidationErrors.New<ToolCommand>(),
            EncodeCommand encodeCommand => Validate(new EncodeCommandValidator(), encodeCommand),
            EncodeUrlCommand encodeUrlCommand => Validate(new EncodeUrlCommandValidator(), encodeUrlCommand),
            EncodeSmsCommand encodeSmsCommand => Validate(new EncodeSmsCommandValidator(), encodeSmsCommand),
            EncodeMailCommand encodeMailCommand => Validate(new EncodeMailCommandValidator(), encodeMailCommand),
            EncodeSepaCommand encodeSepaCommand => Validate(new EncodeSepaCommandValidator(), encodeSepaCommand),
            EncodeWifiCommand encodeWifiCommand => Validate(new EncodeWifiCommandValidator(), encodeWifiCommand),
            EncodeLocationCommand encodeLocationCommand => Validate(new EncodeLocationCommandValidator(), encodeLocationCommand),
            DecodeCommand decodeCommand => Validate(new DecodeCommandValidator(), decodeCommand),
            _ => throw new QrCodeCliException($"Unexpected command type {typeof(TCommand)}")
        };
    }

    private static ValidationErrors Validate<TCommand>(IValidator<TCommand> validator, TCommand command) where TCommand : AbstractCommand
    {
        var errors = validator
            .Validate(command)
            .Errors;
        return ValidationErrors.New<TCommand>(errors);
    }
}