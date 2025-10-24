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

        var alarmes = new List<AlarmeDTO>();
        if (dados != null)
        {
            foreach (var alarme in dados)
            {
                if (alarme.TipoPermissao) // Filtra apenas os alarmes do proprietário
                    alarmes.Add(alarme);
            }
        }

        return alarmes;
    }

    /// <summary>
    /// Cadastra uma nova pessoa com os alarmes selecionados
    /// </summary>
    /// <param name="nome">Nome da pessoa cadastrada</param>
    /// <param name="senha">Senha da pessoa cadastrada</param>
    /// <param name="alarmes">Lista de alarmes relacionados à pessoa cadastrada</param>
    /// <returns>Booleano indicando sucesso ou falha</returns>
    public async Task<bool> CadastrarAsync(string nome, string email, string senha, List<AlarmeDTO> alarmes)
    {
        List<int> idAlarmes = new List<int>();

        foreach (var alarme in alarmes)
            idAlarmes.Add(alarme.Id);

        var status = await _apiService.PostConsultaAsync($"cadastrodto/{int.Parse(Preferences.Get("UserId", "0"))}/cadastro/addacesso", new CadastroAddAcessoDTO
        {
            Usuario = new CadastroDTO
            {
                Nome = nome,
                Email = email,
                Senha = senha
            },
            Alarmes = idAlarmes
        });
        
        bool sucesso = status != null && status.IsSuccessStatusCode;

        return sucesso;
    }
}