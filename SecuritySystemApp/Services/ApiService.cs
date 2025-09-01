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
        //_httpClient.BaseAddress = new Uri("http://localhost:5000/api/"); // URL base da API (Windows localhost)
        _httpClient.BaseAddress = new Uri("http://192.168.15.63:5000/api/"); // URL base da API (Android mesma rede wifi)

        // Adiciona o token no cabeçalho Authorization, se existir
        var token = Preferences.Get("AuthToken", null);
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    // Método GET
    // TResult: Tipo do objeto esperado na resposta
    public async Task<(TResult? Result, HttpResponseMessage? Response)> GetConsultaAsync<TResult>(string httppath)
        where TResult : class
    {
        try
        {
            var resposta = await _httpClient.GetAsync(httppath);

            if (!resposta.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erro ao fazer a requisição: {resposta.ReasonPhrase}");
                return (null, resposta);
            }

            var json = await resposta.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return (result, resposta);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            return (null, null);
        }
    }

    // Método POST básico
    public async Task<HttpResponseMessage?> PostConsultaAsync<T>(string httppath, T dados)
    {
        try
        {
            var resposta = await _httpClient.PostAsJsonAsync(httppath, dados);

            if (!resposta.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erro ao fazer a requisição: {resposta.ReasonPhrase}");
            }

            return resposta;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            return null;
        }
    }

    // Método POST que retorna objeto desserializado
    // T: Tipo do objeto a ser enviado
    // Result: Tipo do objeto esperado na resposta
    // Response: Resposta de status Http
    public async Task<(TResult? Result, HttpResponseMessage? Response)> PostConsultaAsync<T, TResult>(string httppath, T dados)
        where T : class
        where TResult : class
    {
        try
        {
            var resposta = await _httpClient.PostAsJsonAsync(httppath, dados);

            if (!resposta.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erro ao fazer a requisição: {resposta.ReasonPhrase}");
                return (null, resposta);
            }

            var json = await resposta.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return (result, resposta);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            return (null, null);
        }
    }

    // Método PUT
    public async Task<HttpResponseMessage?> PutConsultaAsync<T>(string httppath, T dados) where T : class
    {
        try
        {
            var resposta = await _httpClient.PutAsJsonAsync(httppath, dados);

            if (!resposta.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erro ao fazer a requisição: {resposta.ReasonPhrase}");
            }

            return resposta;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            return null;
        }
    }

    // Método DELETE
    public async Task<HttpResponseMessage?> DeleteConsultaAsync(string httppath)
    {
        try
        {
            var resposta = await _httpClient.DeleteAsync(httppath);

            if (!resposta.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erro ao fazer a requisição: {resposta.ReasonPhrase}");
            }

            return resposta;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            return null;
        }
    }
}
