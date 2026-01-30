using Bogus;
using Financas.Communication.Request;

namespace CommonTestUtilities.Requests;
public class RequestLoginJsonBuilder
{
    public static RequestLoginJson Build()
    {
        return new Faker<RequestLoginJson>()
            .RuleFor(user => user.Email, faker => faker.Internet.Email())
            .RuleFor(user => user.Senha, faker => faker.Internet.Password(prefix: "24083066Jj*"));
          
    }
}
