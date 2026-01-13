using Bogus;
using Financas.Communication.Request;

namespace CommonTestUtilities;
public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Nome, faker => faker.Person.FirstName)
            .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Nome))
            .RuleFor(user => user.Senha, faker => faker.Internet.Password(prefix:"2Jj*"));
    }


}
