using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class LoginViewModel
{
    private readonly AuthService _authService;

    public LoginViewModel()
    {
        // Definição dos Serviços
        _authService = new AuthService();
    }

    // Método para realizar login
    public async Task<bool> Entrar(string email, string senha)
    {
        return await _authService.LoginAsync(email, senha);
    }
}