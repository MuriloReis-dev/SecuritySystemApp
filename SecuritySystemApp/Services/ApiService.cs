using System.Reflection;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SecuritySystemApp.Services;

// Classe para consumir a API e fazer requisições ao servidor do banco
public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5000/api/"); // URL base da API
    }

    // Método GET
    public async Task<List<T>> LerConsultasAsync<T>() where T : class
    {
        try
        {
            var resposta = await _httpClient.GetFromJsonAsync<List<T>>("usuarios/get");
            return resposta ?? new List<T>();
        }
        catch (HttpRequestException ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            return new List<T>();
        }
    }
}
