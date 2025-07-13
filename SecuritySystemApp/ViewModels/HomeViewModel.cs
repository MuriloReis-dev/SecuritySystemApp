using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Models;
using SecuritySystemApp.Services;

namespace SecuritySystemApp.ViewModels;

public class HomeViewModel
{
    public async Task<List<Alarme>> CarregarAlarmesAsync()
    {
        var service = new LeituraDBService();
        var dados = await service.CarregarAsync<Alarme>();
        return dados;
    }
}