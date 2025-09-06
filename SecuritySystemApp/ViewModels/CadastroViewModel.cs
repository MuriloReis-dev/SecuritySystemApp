using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class CadastroViewModel
{
    private readonly AuthService _authService;

    public CadastroViewModel()
    {
        // Definição dos Serviços
        _authService = new AuthService();
    }

    // Método para realizar cadastro
    public async Task<bool> Cadastrar(string nome, string email, string senha)
    {
        var sucesso = await _authService.RegisterAsync(nome, email, senha);

        if (sucesso)
        {
            return await _authService.LoginAsync(email, senha);
        }

        return false;
    }
}