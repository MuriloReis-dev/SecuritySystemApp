using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class HomeViewModel
{
    private readonly ApiService _apiService;

    public HomeViewModel()
    {
        // Definição dos Serviços
        _apiService = new ApiService();
    }

    public async Task<List<EntradasDTO>?> GerarDadosGrafico()
    {
        var (dados, status) = await _apiService.GetConsultaAsync<List<EntradasDTO>>($"entradadto/{int.Parse(Preferences.Get("UserId", "0"))}/listagem"); // URL do endpoint para obter os dados do gráfico

        return dados;
    }

    /// <summary>
    /// Carrega a lista de alarmes do usuário
    /// </summary>
    /// <returns>Lista de alarmes</returns>
    public async Task<List<AlarmeDTO>?> CarregarAlarmesAsync()
    {
        var (dados, status) = await _apiService.GetConsultaAsync<List<AlarmeDTO>>($"alarmedto/{int.Parse(Preferences.Get("UserId", "0"))}/listagem"); // URL do endpoint para obter os alarmes

        return dados;
    }
}