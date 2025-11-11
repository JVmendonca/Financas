using Bogus;
using Financas.Communication.Enuns;
using Financas.Communication.Request;

namespace CommonTestUtilities;
public class RequestDispesaJsonBuilder
{
    public static RequestDispesaJson Build()
    {
        return new Faker<RequestDispesaJson>()
             .RuleFor(r => r.Titulo, f => f.Commerce.ProductName())
             .RuleFor(r => r.Descricao, f => f.Lorem.Paragraph())
             .RuleFor(r => r.Data, f => f.Date.Past())
             .RuleFor(r => r.Valor, f => f.Random.Decimal(min: 1, max: 1000))
             .RuleFor(r => r.Pagamento, f => f.PickRandom<PaymentType>());
    }
}
