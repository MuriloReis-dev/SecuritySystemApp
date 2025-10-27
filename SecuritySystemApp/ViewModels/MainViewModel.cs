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
}