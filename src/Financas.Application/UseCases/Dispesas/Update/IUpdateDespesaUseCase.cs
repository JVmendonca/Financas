using Financas.Communication.Request;

namespace Financas.Application.UseCases.Dispesas.Update;
public interface IUpdateDespesaUseCase
{ 
    Task Execute(long id, RequestDispesaJson request);
}
