using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class MainViewModel
{
    // Teste de conexão com a API
    public async Task<List<Usuario>> CarregarUsuariosAsync()
    {
        var service = new ApiService();
        var dados = await service.LerConsultasAsync<Usuario>();

        return dados;
    }
}