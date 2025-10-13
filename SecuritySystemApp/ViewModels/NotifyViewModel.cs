using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class NotifyViewModel
{
    private readonly ApiService _apiService;

    public NotifyViewModel()
    {
        // Definição dos Serviços
        _apiService = new ApiService();
    }

    /// <summary>
    /// Carrega as notificações do usuário
    /// </summary>
    /// <returns>Lista de Notificações</returns>
    public async Task<List<NotifyDTO>?> CarregarNotificacoesAsync()
    {
        var (result, response) = await _apiService.GetConsultaAsync<List<NotifyDTO>>($"notifydto/{int.Parse(Preferences.Get("UserId", "0"))}/listagem");

        return result;
    }
}