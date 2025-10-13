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

    /// <summary>
    /// Realiza o login do usuário
    /// </summary>
    /// <param name="email">Email do usuário</param>
    /// <param name="senha">Senha do usuário</param>
    /// <returns>Booleano indicando sucesso ou falha</returns>
    public async Task<bool> Entrar(string email, string senha)
    {
        return await _authService.LoginAsync(email, senha);
    }
}