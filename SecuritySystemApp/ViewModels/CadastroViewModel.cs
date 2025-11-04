using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.ViewModels;

public class CadastroViewModel
{
    private readonly AuthService _authService;
    private readonly FirebaseService _firebaseService;

    public CadastroViewModel()
    {
        // Definição dos Serviços
        _authService = new AuthService();
        _firebaseService = new FirebaseService();
    }

    /// <summary>
    /// Cadastra um novo usuário e realiza o login
    /// </summary>
    /// <param name="nome">Nome do usuário cadastrado</param>
    /// <param name="email">Email do usuário cadastrado</param>
    /// <param name="senha">Senha do usuário cadastrado</param>
    /// <returns>Booleano indicando sucesso ou falha</returns>
    public async Task<bool> Cadastrar(string nome, string email, string senha)
    {
        var sucesso = await _authService.RegisterAsync(nome, email, senha);

        if (sucesso)
        {
            var tokenFmc = await _firebaseService.GetTokenAsync() ?? string.Empty;
            return await _authService.LoginAsync(email, senha, tokenFmc);
        }

        return false;
    }
}