using Financas.Domain.Entidades;

namespace Financas.Domain.Repositorios.Dispesas;
public interface IDespesasRepositorio
{
   Task add(Dispesa dispesa);
   Task<List<Dispesa>> GetAll();

}
