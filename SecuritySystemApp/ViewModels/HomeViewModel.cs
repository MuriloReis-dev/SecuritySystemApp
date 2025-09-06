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

    // Propriedade para armazenar a lista de alarmes
    public async Task<List<Alarme>> CarregarAlarmesAsync()
    {
        var (dados, status) = await _apiService.GetConsultaAsync<List<Alarme>>("alarmes/get"); // URL do endpoint para obter os alarmes

        // Simulação de dados para teste, remover após implementar a API
        dados = new List<Alarme>
        {
            new Alarme { Id = "1", Nome = "Alarme 1", Ligado = true, DataHora = DateTime.Now },
            new Alarme { Id = "2", Nome = "Alarme 2", Ligado = false, DataHora = DateTime.Now.AddMinutes(-30) },
            new Alarme { Id = "3", Nome = "Alarme 3", Ligado = true, DataHora = DateTime.Now.AddHours(-1) },
            new Alarme { Id = "4", Nome = "Alarme 4", Ligado = true, DataHora = DateTime.Now },
            new Alarme { Id = "5", Nome = "Alarme 5", Ligado = false, DataHora = DateTime.Now.AddMinutes(-30) },
            new Alarme { Id = "6", Nome = "Alarme 6", Ligado = true, DataHora = DateTime.Now.AddHours(-1) }
        }.Cast<Alarme>().ToList();

        return dados;
    }
}