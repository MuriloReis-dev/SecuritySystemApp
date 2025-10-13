namespace SecuritySystemApp.Models;

// Model que retorna a resposta ao logar, com dados do usuário e um token de autenticação
public class NotifyDTO
{
    public int Id { get; set; }
    public string? Mensagem { get; set; }
    public DateTime DataHora { get; set; }
}