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

    // Método para realizar login
    // Retorna o e um booleano indicando sucesso ou falha
    public async Task<bool> LoginAsync(string email, string senha)
    {
        var login = new LoginDTO
        {
            Email = email,
            Senha = senha
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

    // Método para realizar cadastro
    // Retorna um booleano indicando sucesso ou falha
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

    // Método para realizar logout
    // Remove o token e informações do usuário das preferências
    public async Task LogoutAsync()
    {
        Preferences.Remove("AuthToken");
        Preferences.Remove("UserId");
        Preferences.Remove("UserName");
        Preferences.Remove("UserEmail");

        await Task.CompletedTask;
    }

    // Método para validar o token armazenado
    // Retorna um booleano indicando se o token é válido ou não
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

        bool sucesso = status != null && status.IsSuccessStatusCode;
        
        if (sucesso)
        {
            await LogoutAsync();
            return false;
        }

        return await Task.FromResult(true);
    }
}