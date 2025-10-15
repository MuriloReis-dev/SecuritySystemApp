using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class MainViewModel
{
    
    private readonly ApiService _apiService;

    public MainViewModel()
    {
        // Definição dos Serviços
        _apiService = new ApiService();
    }

    // Apenas para teste de gráfico
    /// <summary>
    /// Gera dados fictícios para o gráfico de entradas
    /// </summary>
    /// <returns>Lista de EntradasDTO</returns>
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
}