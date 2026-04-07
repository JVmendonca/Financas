using Financas.Communication.Request;

namespace Financas.Application.UseCases.Despesas.Update;
public interface IUpdateDespesaUseCase
{ 
    Task Execute(long id, RequestDespesaJson request);
}
