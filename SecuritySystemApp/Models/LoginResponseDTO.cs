namespace SecuritySystemApp.Models;

// Model que retorna a resposta ao logar, com dados do usuário e um token de autenticação
public class LoginResponseDTO
{
    public Usuario? Usuario { get; set; }
    public string? Token { get; set; }
}