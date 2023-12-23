using App.Commands;
using App.Validators;
using FluentAssertions;
using FluentValidation.Results;

namespace Tests.Validators;

public class ValidationErrorTests
{
    [Theory]
    [InlineData(nameof(EncodeCommand.Input), "Required option", "-i|--input")]
    public void Should_Get_Valid_OptionName(string propertyName, string errorMessage, string expectedOptionName)
    {
        // arrange
        var validationFailure = new ValidationFailure(propertyName, errorMessage);
        var validationError = ValidationError.New<EncodeCommand>(validationFailure);

        // act
        var optionName = validationError.OptionName();

        // assert
        optionName.Should().NotBeNullOrWhiteSpace();
        optionName.Should().Be(expectedOptionName);
    }
}