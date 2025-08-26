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

        // Adiciona o token no cabeçalho Authorization, se existir
        var token = Preferences.Get("AuthToken", null);
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    // Método GET
    // T: Tipo do objeto esperado na resposta
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

    // Método POST básico
    public async Task<HttpResponseMessage> PostConsultaAsync<T>(string httppath, T dados)
    {
        var resposta = await _httpClient.PostAsJsonAsync(httppath, dados);

        if(!resposta.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {resposta.ReasonPhrase}");
        }

        return resposta;
    }

    // Método POST que retorna objeto desserializado
    // T: Tipo do objeto a ser enviado
    // Result: Tipo do objeto esperado na resposta
    // Response: Resposta de status Http
    public async Task<(TResult? Result, HttpResponseMessage Response)> PostConsultaAsync<T, TResult>(string httppath, T dados)
        where T : class
        where TResult : class
    {
        var resposta = await _httpClient.PostAsJsonAsync(httppath, dados);

        if(!resposta.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {resposta.ReasonPhrase}");
            return (null, resposta);
        }

        var json = await resposta.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return (result, resposta);
    }

    // Método PUT
    public async Task<HttpResponseMessage> PutConsultaAsync<T>(string httppath, T dados) where T : class
    {
        var resposta = await _httpClient.PutAsJsonAsync(httppath, dados);

        if(!resposta.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {resposta.ReasonPhrase}");
        }

        return resposta;
    }

    // Método DELETE
    public async Task<HttpResponseMessage> DeleteConsultaAsync(string httppath)
    {
        var resposta = await _httpClient.DeleteAsync(httppath);

        if(!resposta.IsSuccessStatusCode)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {resposta.ReasonPhrase}");
        }

        return resposta;
    }
}
