using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class CadastroViewModel
{
    public async Task<bool> CadastrarUsuarioAsync(string nome, string email, string senha)
    {
        var service = new ApiService();

        var usuario = new CadastroDTO
        {
            Nome = nome,
            Email = email,
            Senha = senha
        };

        var resposta = await service.PostConsultaAsync("cadastro/post", usuario); // URL do endpoint de cadastro

        return resposta.IsSuccessStatusCode;
    }
}