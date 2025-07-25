using System.Reflection;
using System.Text.Json;

namespace SecuritySystemApp.Services;

// Classe para converter dados do json para uma lista de dicionários, simulando uma tabela
public class LeituraDBService
{
    // converter os dados para string antes de retornar valor para que o DataTrigger funcione
    public async Task<List<Dictionary<string, object>>> LerConsultasAsync()
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "SecuritySystemApp.Resources.Data.leituras.json";

            using Stream? stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new Exception($"Recurso não encontrado: {resourceName}");

            using StreamReader reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            var consulta = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

            return consulta ?? new List<Dictionary<string, object>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
            return new List<Dictionary<string, object>>();
        }
    }
}
