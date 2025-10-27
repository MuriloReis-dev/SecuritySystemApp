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

    public List<EntradasDTO> GerarDadosGrafico()
    {
        var dados = new List<EntradasDTO>
        {
            new EntradasDTO { Data = DateTime.Now.AddDays(-6), QtdEntradas = 5 },
            new EntradasDTO { Data = DateTime.Now.AddDays(-5), QtdEntradas = 8 },
            new EntradasDTO { Data = DateTime.Now.AddDays(-4), QtdEntradas = 2 },
            new EntradasDTO { Data = DateTime.Now.AddDays(-3), QtdEntradas = 10 },
            new EntradasDTO { Data = DateTime.Now.AddDays(-2), QtdEntradas = 7 },
            new EntradasDTO { Data = DateTime.Now.AddDays(-1), QtdEntradas = 4 },
            new EntradasDTO { Data = DateTime.Now, QtdEntradas = 9 }
        };

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