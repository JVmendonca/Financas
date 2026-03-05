using Bogus;
using CommonTestUtilities.Cryptography;
using Financas.Domain.Entidades;
using Financas.Domain.Enums;

namespace CommonTestUtilities.Entites;
public class UserBuild
{
    public static User Build(string role = Regras.TIME_MEMBRO)
    {
        var passwordEncrypt = new PasswordEncripterBuilder().Build();

        var user = new Faker<User>()
            .RuleFor(u => u.Id, _ => 1)
            .RuleFor(u => u.Nome, faker => faker.Person.FirstName)
            .RuleFor(u => u.Email, (faker, user) => faker.Internet.Email(user.Email))
            .RuleFor(u => u.Senha, (_, user) => passwordEncrypt.Encrypt(user.Senha))
            .RuleFor(u => u.UserIndetificador, _ => Guid.NewGuid())
            .RuleFor(u => u.Regra, _ => role);

        return user;
    }
}       
