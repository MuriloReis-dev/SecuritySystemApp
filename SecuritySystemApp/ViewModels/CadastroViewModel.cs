using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class CadastroViewModel
{
    // Método para realizar cadastro
    // Retorna o usuário cadastrado e um booleano indicando sucesso ou falha
    public async Task<(Usuario?, bool)> CadastrarAsync(string nome, string email, string senha)
    {
        var service = new ApiService();

        var cadastro = new CadastroDTO
        {
            Nome = nome,
            Email = email,
            Senha = senha
        };

        var (usuario, status) = await service.PostConsultaAsync<CadastroDTO, Usuario>("cadastro/post", cadastro); // URL do endpoint de cadastro

        bool sucesso = status != null && status.IsSuccessStatusCode;

        return (usuario, sucesso);
    }
}