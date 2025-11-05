using Financas.Communication.Enuns;
using Financas.Communication.Request;
using Financas.Communication.Responses;

namespace Financas.Application.UseCases.Dispesas.Register;
public class RegisterDispesasUseCase
{
    public ResponseDispesaJson Execute(RequestDispesaJson request)
    {
        Validate(request);

        return new ResponseDispesaJson();
    }

    private void Validate(RequestDispesaJson request)
    {
        var tituloIsEmpty = string.IsNullOrWhiteSpace(request.Titulo);
        if (tituloIsEmpty)
        {
            throw new ArgumentException("O título da dispesa é obrigatório.");
        }

        if (request.Valor <= 0)
        {
            throw new ArgumentException("O valor da dispesa deve ser maior que zero.");
        }

        var dataFutura = DateTime.Compare(request.Data, DateTime.UtcNow);
        if (dataFutura > 0)
        {
            throw new ArgumentException("A data da dispesa não pode ser futura.");

        }

        var PagamentoValido = Enum.IsDefined(typeof(PaymentType), request.Pagamento);
        if (PagamentoValido == false)
        {
            throw new ArgumentException("O tipo de pagamento da dispesa é inválido.");
        }


    }

}
