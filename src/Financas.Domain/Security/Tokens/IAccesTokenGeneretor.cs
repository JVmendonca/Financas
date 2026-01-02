namespace Financas.Domain.Security.Tokens;
public interface IAccesTokenGeneretor
{
    string Generate(Entidades.User user);
}
