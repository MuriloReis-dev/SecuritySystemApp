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
        _httpClient.BaseAddress = new Uri("https://apisecuritysystem.onrender.com/api/"); // URL base da API (Windows localhost)
        _httpClient.Timeout = TimeSpan.FromSeconds(30);

        // Adiciona o token no cabeçalho Authorization, se existir
        var token = Preferences.Get("AuthToken", null);
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    /// <summary>
    /// Método GET que retorna objeto desserializado
    /// </summary>
    /// <typeparam name="TResult">Tipo do objeto esperado na resposta</typeparam>
    /// <param name="httppath">Caminho url para a requisição na API</param>
    /// <returns>Tupla com resultado da requisição e resposta HTTP</returns>
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
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"GET timeout: {httppath} - {ex}");
            return (null, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GET unexpected error: {httppath} - {ex}");
            return (null, null);
        }
    }

    /// <summary>
    /// Método POST
    /// </summary>
    /// <typeparam name="T">Tipo de dado enviado na requisição</typeparam>
    /// <param name="httppath">Caminho url para a requisição na API</param>
    /// <param name="dados">Dados enviados na requisição</param>
    /// <returns>Resposta HTTP</returns>
    public async Task<HttpResponseMessage?> PostConsultaAsync<T>(string httppath, T? dados)
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
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"GET timeout: {httppath} - {ex}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GET unexpected error: {httppath} - {ex}");
            return null;
        }
    }

    /// <summary>
    /// Método POST que retorna objeto desserializado
    /// </summary>
    /// <typeparam name="T">Tipo de dado enviado na requisição</typeparam>
    /// <typeparam name="TResult">Tipo do objeto esperado na resposta</typeparam>
    /// <param name="httppath">Caminho url para a requisição na API</param>
    /// <param name="dados">Dados enviados na requisição</param>
    /// <returns>Tupla com resultado da requisição e resposta HTTP</returns>
    public async Task<(TResult? Result, HttpResponseMessage? Response)> PostConsultaAsync<T, TResult>(string httppath, T dados)
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
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"GET timeout: {httppath} - {ex}");
            return (null, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GET unexpected error: {httppath} - {ex}");
            return (null, null);
        }
    }

    /// <summary>
    /// Método PUT
    /// </summary>
    /// <typeparam name="T">Tipo de dado enviado na requisição</typeparam>
    /// <param name="httppath">Caminho url para a requisição na API</param>
    /// <param name="dados">Dados enviados na requisição</param>
    /// <returns>Resposta HTTP</returns>
    public async Task<HttpResponseMessage?> PutConsultaAsync<T>(string httppath, T dados)
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
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"GET timeout: {httppath} - {ex}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GET unexpected error: {httppath} - {ex}");
            return null;
        }
    }

    /// <summary>
    /// Método DELETE
    /// </summary>
    /// <param name="httppath">Caminho url para a requisição na API</param>
    /// <returns>Resposta HTTP</returns>
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
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"GET timeout: {httppath} - {ex}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GET unexpected error: {httppath} - {ex}");
            return null;
        }
    }
}
