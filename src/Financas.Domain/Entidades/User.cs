namespace Financas.Domain.Entidades;
public class User
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public Guid UserIndetificador { get; set; }
    public string Regra { get; set; } = string.Empty;
}
