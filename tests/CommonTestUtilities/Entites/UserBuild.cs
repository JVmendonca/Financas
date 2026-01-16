using Bogus;
using CommonTestUtilities.Cryptography;
using Financas.Domain.Entidades;

namespace CommonTestUtilities.Entites;
public class UserBuild
{
    public static User Build()
    {
        var passwordEncrypt = PasswordEncripterBuilder.Build();

        var user = new Faker<User>()
            .RuleFor(u => u.Id, _ => 1)
            .RuleFor(u => u.Nome, faker => faker.Person.FirstName)
            .RuleFor(u => u.Email, (faker, user) => faker.Internet.Email(user.Email))
            .RuleFor(u => u.Senha, (_, user) => passwordEncrypt.Encrypt(user.Senha))
            .RuleFor(u => u.UserIndetificador, _ => Guid.NewGuid());

        return user;
    }
        
