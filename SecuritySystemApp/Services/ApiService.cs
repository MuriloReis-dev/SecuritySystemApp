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
    public async Task<List<T>> GetConsultaAsync<T>(string httppath) where T : class
    {
        try
        {
            var resposta = await _httpClient.GetFromJsonAsync<List<T>>(httppath);
            return resposta ?? new List<T>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            return new List<T>();
        }
    }

    // Método POST
    public async Task<HttpResponseMessage> PostConsultaAsync<T>(string httppath, T dados) where T : class
    {
        try
        {
            var resposta = await _httpClient.PostAsJsonAsync(httppath, dados);
            resposta.EnsureSuccessStatusCode();
            return resposta;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            throw;
        }
    }

    // Método PUT
    public async Task<HttpResponseMessage> PutConsultaAsync<T>(string httppath, T dados) where T : class
    {
        try
        {
            var resposta = await _httpClient.PutAsJsonAsync(httppath, dados);
            resposta.EnsureSuccessStatusCode();
            return resposta;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            throw;
        }
    }

    // Método DELETE
    public async Task<HttpResponseMessage> DeleteConsultaAsync(string httppath)
    {
        try
        {
            var resposta = await _httpClient.DeleteAsync(httppath);
            resposta.EnsureSuccessStatusCode();
            return resposta;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            throw;
        }
    }
}
