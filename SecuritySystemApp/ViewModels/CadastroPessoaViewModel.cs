using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SecuritySystemApp.Services;
using System.Linq;
using System.Threading.Tasks;
using SecuritySystemApp.Models;
using System;

namespace SecuritySystemApp.ViewModels;

public class CadastroPessoaViewModel
{
    private readonly ApiService _apiService;
    public CadastroPessoaViewModel()
    {
        // Definição dos Serviços
        _apiService = new ApiService();
    }

    public async Task<List<AlarmeDTO>?> CarregarAlarmesAsync()
    {
        var (dados, status) = await _apiService.GetConsultaAsync<List<AlarmeDTO>>($"alarmedto/{int.Parse(Preferences.Get("UserId", "0"))}/listagem"); // URL do endpoint para obter os alarmes

        return dados;
    }

    public async Task<bool> CadastrarAsync(string nome, string senha, List<AlarmeDTO> alarmes)
    {
        Console.WriteLine($"Nome: {nome}");
        Console.WriteLine($"Senha: {senha}");
        Console.WriteLine($"Alarmes:");
        foreach (var alarme in alarmes)
            Console.WriteLine(alarme.Nome);

        return await Task.FromResult(false);
    }
}