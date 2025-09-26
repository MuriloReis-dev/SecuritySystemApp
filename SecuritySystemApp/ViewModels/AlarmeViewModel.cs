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

    public async Task<AlarmeDetailsDTO?> CarregarAlarmeAsync(int id_alarme)
    {
        var (result, response) = await _apiService.GetConsultaAsync<AlarmeDetailsDTO>($"alarmedto/{id_alarme}/{int.Parse(Preferences.Get("UserId", "0"))}/detalhes");

        return result;
    }

    // Propriedade para ligar/desligar alarme
    public async Task<bool> AlarmeOnOffAsync(int id_alarme, bool ligado)
    {
        var resposta = await _apiService.PutConsultaAsync($"alarmedto/{id_alarme}/toggle", ligado);

        return resposta != null && resposta.IsSuccessStatusCode;
    }
}