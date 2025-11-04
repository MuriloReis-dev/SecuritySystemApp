using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.Services;

public class AuthService
{
    private readonly ApiService _apiService;

    public AuthService()
    {
        _apiService = new ApiService();
    }

    /// <summary>
    /// Método para realizar login
    /// </summary>
    /// <param name="email">Email do usuário</param>
    /// <param name="senha">Senha do Usuário</param>
    /// <returns>Booleano indicando sucesso ou falha</returns>
    public async Task<bool> LoginAsync(string email, string senha, string tokenFmc)
    {
        var login = new LoginDTO
        {
            Email = email,
            Senha = senha,
            TokenFMC = tokenFmc
        };

        var (loginResponse, status) = await _apiService.PostConsultaAsync<LoginDTO, LoginResponseDTO>("logindto/login", login); // URL do endpoint de login

        // Token para o usuário continuar logado (gerado na API)
        if (loginResponse?.Token != null && loginResponse.Usuario != null)
        {
            Preferences.Set("AuthToken", loginResponse.Token);
            Preferences.Set("UserId", loginResponse.Usuario.Id.ToString());
            Preferences.Set("UserName", loginResponse.Usuario.Nome);
            Preferences.Set("UserEmail", loginResponse.Usuario.Email);

            Console.WriteLine($"Token armazenado: {Preferences.Get("AuthToken", string.Empty)}");
            Console.WriteLine($"UserId armazenado: {Preferences.Get("UserId", string.Empty)}");
            Console.WriteLine($"UserName armazenado: {Preferences.Get("UserName", string.Empty)}");
            Console.WriteLine($"UserEmail armazenado: {Preferences.Get("UserEmail", string.Empty)}");
        }

        bool sucesso = status != null && status.IsSuccessStatusCode;

        return sucesso;
    }

    /// <summary>
    /// Método para realizar cadastro
    /// </summary>
    /// <param name="nome">Nome do usuário cadastrado</param>
    /// <param name="email">Email do usuário cadastrado</param>
    /// <param name="senha">Senha do usuário cadastrado</param>
    /// <returns>Booleano indicando sucesso ou falha</returns>
    public async Task<bool> RegisterAsync(string nome, string email, string senha)
    {
        var cadastro = new CadastroDTO
        {
            Nome = nome,
            Email = email,
            Senha = senha
        };

        var status = await _apiService.PostConsultaAsync("cadastrodto/cadastro", cadastro); // URL do endpoint de cadastro

        bool sucesso = status != null && status.IsSuccessStatusCode;

        return sucesso;
    }

    /// <summary>
    /// Método para realizar logout
    /// </summary>
    public async Task LogoutAsync()
    {
        Preferences.Remove("AuthToken");
        Preferences.Remove("UserId");
        Preferences.Remove("UserName");
        Preferences.Remove("UserEmail");

        await Task.CompletedTask;
    }

    /// <summary>
    /// Método para validar o token armazenado
    /// </summary>
    /// <returns>Booleano indicando sucesso ou falha</returns>
    public async Task<bool> ValidateLoginAsync()
    {
        var token = Preferences.Get("AuthToken", null);

        // Verifica se o token foi definido
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        // Validação do token na API
        var status = await _apiService.PostConsultaAsync("auth/validar", new Usuario
        {
            Id = int.Parse(Preferences.Get("UserId", "0")),
            Nome = Preferences.Get("UserName", string.Empty),
            Email = Preferences.Get("UserEmail", string.Empty)
        });

        Console.WriteLine($"Status da validação do token: {status?.StatusCode}");

        bool sucesso = status != null && status.IsSuccessStatusCode;

        if (!sucesso)
        {
            await LogoutAsync();
            return false;
        }

        return await Task.FromResult(true);
    }
}