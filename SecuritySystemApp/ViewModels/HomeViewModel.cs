using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;

namespace SecuritySystemApp.ViewModels;

public class HomeViewModel
{
    public async Task<List<Dictionary<string, string>>> CarregarAlarmesAsync()
    {
        var service = new LeituraDBService();
        var dadosObj = await service.LerConsultasAsync();

        // Converte dados para string para o DataTrigger funcionar
        var dados = dadosObj.Select(dict => dict.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.ToString() ?? string.Empty)).ToList();
        
        return dados;
    }
}