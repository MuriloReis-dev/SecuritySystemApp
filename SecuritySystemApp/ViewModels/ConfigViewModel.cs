using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class ConfigViewModel
{
    private readonly ApiService _apiService;

    public ConfigViewModel()
    {
        // Definição dos Serviços
        _apiService = new ApiService();
    }

    public async Task<bool> AlterarSenhaAsync(string senhaAtual, string novaSenha)
    {
        var senhas = new
        {
            SenhaAtual = senhaAtual,
            NovaSenha = novaSenha
        };

        var resposta = await _apiService.PutConsultaAsync($"auth/{Preferences.Get("UserId", "0")}/editsenha", senhas);

        return resposta != null && resposta.IsSuccessStatusCode;
    }
}
