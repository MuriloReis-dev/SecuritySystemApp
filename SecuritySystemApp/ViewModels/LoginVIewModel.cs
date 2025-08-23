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

        var (loginResponse, status) = await service.PostConsultaAsync<LoginDTO, LoginResponseDTO>("login/post", login); // URL do endpoint de login

        // Token para o usuário continuar logado (gerado na API)
        if (loginResponse?.Token != null)
        {
            Preferences.Set("AuthToken", loginResponse.Token);
        }

        bool sucesso = status != null && status.IsSuccessStatusCode;

        return (loginResponse?.Usuario, sucesso);
    }
}