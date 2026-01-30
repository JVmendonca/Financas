using Bogus;
using Financas.Domain.Entidades;
using Financas.Domain.Enuns;



namespace CommonTestUtilities.Entites;
public class DespesasBuilder
{
    public static List<Dispesa> Collection(User user, uint cout = 2)
    {
        var list = new List<Dispesa>();
        
        if (cout == 0)
            cout = 1;

        var despesaId = 1;

        for (int i = 0; i < cout; i++)
        {
            var despesa = Build(user);
            despesa.Id = despesaId++;

            list.Add(despesa);
        }

        return list;
    }

    public static Dispesa Build(User user)
    {
        return new Faker<Dispesa>()
            .RuleFor(u => u.Id, _ => 1)
            .RuleFor(u => u.Titulo, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Descricao, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Data, faker => faker.Date.Past())
            .RuleFor(r => r.Valor, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(r => r.Pagamento, faker => faker.PickRandom<PaymentType>())
            .RuleFor(r => r.UserId, _ => user.Id);
    }
}
