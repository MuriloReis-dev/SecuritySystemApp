using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class AlarmeViewModel
{
    private readonly ApiService _apiService;

    public AlarmeViewModel()
    {
        // Definição dos Serviços
        _apiService = new ApiService();
    }

    // Propriedade para armazenar a lista de alarmes
    public async Task AlarmeOnOffAsync(string id_alarme, bool ligado)
    {
        var resposta = await _apiService.PutConsultaAsync($"alarmedto/{id_alarme}/toggle", new { Ligado = ligado });
    }
}