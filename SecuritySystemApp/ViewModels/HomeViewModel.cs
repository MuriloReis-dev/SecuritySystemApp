using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;

namespace SecuritySystemApp.ViewModels;

public class HomeViewModel
{
    public async Task<List<Dictionary<string, object>>> CarregarAlarmesAsync()
    {
        var service = new LeituraDBService();
        var dados = await service.LerConsultasAsync();
        return dados;
    }
}