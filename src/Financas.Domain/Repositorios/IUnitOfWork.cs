namespace Financas.Domain.Repositorios;
public interface IUnitOfWork
{
    Task Commit();
}
