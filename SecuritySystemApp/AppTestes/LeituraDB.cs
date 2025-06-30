using System.Reflection;
using System.Text.Json;

namespace SecuritySystemApp.AppTestes;

// Classe para converter dados do json para um objeto
public class LeituraDB
{
    public async Task<List<Alarme>> CarregarAsync()
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "SecuritySystemApp.AppTestes.leituras.json";

            using Stream? stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new Exception($"Recurso não encontrado: {resourceName}");

            using StreamReader reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            var resultado = JsonSerializer.Deserialize<TabelasDB>(json);
            return resultado?.Alarme ?? new List<Alarme>();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
            return new List<Alarme>();
        }
    }
}