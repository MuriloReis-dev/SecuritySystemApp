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

    /// <summary>
    /// Carrega alarme selecionado
    /// </summary>
    /// <param name="id_alarme">Id do alarme selecionado</param>
    /// <returns>Informações sobre o alarme</returns>
    public async Task<AlarmeDetailsDTO?> CarregarAlarmeAsync(int id_alarme)
    {
        var (result, response) = await _apiService.GetConsultaAsync<AlarmeDetailsDTO>($"alarmedto/{id_alarme}/{int.Parse(Preferences.Get("UserId", "0"))}/detalhes");

        return result;
    }

    /// <summary>
    /// Liga/Desliga o alarme
    /// </summary>
    /// <param name="id_alarme">Id do alarme selecionado</param>
    /// <param name="ligado">Novo estado do alarme</param>
    /// <returns>Booleano indicando sucesso ou falha</returns>
    public async Task<bool> AlarmeOnOffAsync(int id_alarme, bool ligado)
    {
        var resposta = await _apiService.PutConsultaAsync($"alarmedto/{id_alarme}/toggle", ligado);

        return resposta != null && resposta.IsSuccessStatusCode;
    }
}