using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class LoginViewModel
{
    // Método para realizar login
    // Retorna o usuário logado e um booleano indicando sucesso ou falha
    public async Task<(Usuario?, bool)> EntrarAsync(string email, string senha)
    {
        var service = new ApiService();

        var login = new LoginDTO
        {
            Email = email,
            Senha = senha
        };

        var (usuario, resposta) = await service.PostConsultaAsync<LoginDTO, Usuario>("login/post", login); // URL do endpoint de login

        bool sucesso = resposta != null && resposta.IsSuccessStatusCode;

        return (usuario, sucesso);
    }
}