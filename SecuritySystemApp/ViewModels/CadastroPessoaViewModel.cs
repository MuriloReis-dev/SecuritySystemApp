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

    /// <summary>
    /// Carrega a lista de alarmes do usuário
    /// </summary>
    /// <returns>Lista de alarmes</returns>
    public async Task<List<AlarmeDTO>?> CarregarAlarmesAsync()
    {
        var (dados, status) = await _apiService.GetConsultaAsync<List<AlarmeDTO>>($"alarmedto/{int.Parse(Preferences.Get("UserId", "0"))}/listagem"); // URL do endpoint para obter os alarmes

        return dados;
    }

    /// <summary>
    /// Cadastra uma nova pessoa com os alarmes selecionados
    /// </summary>
    /// <param name="nome">Nome da pessoa cadastrada</param>
    /// <param name="senha">Senha da pessoa cadastrada</param>
    /// <param name="alarmes">Lista de alarmes relacionados à pessoa cadastrada</param>
    /// <returns>Booleano indicando sucesso ou falha</returns>
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