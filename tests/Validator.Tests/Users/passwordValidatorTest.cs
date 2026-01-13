using Financas.Application.UseCases.User;
using Financas.Communication.Request;
using FluentAssertions;
using FluentValidation;

namespace Validator.Tests.Users;
public class passwordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaa1")]
    public void Error_Password_Invalid(string senha)
    {
        //Arrange
        var validator = new SenhaValidator<RequestRegisterUserJson>();

        //Act
        var result = validator
            .IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), senha);

        //Assert
        result.Should().BeFalse();
    }
}
