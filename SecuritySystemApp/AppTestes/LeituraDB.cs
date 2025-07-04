using System.Reflection;
using System.Text.Json;

namespace SecuritySystemApp.AppTestes;

// Classe para converter dados do json para um objeto
public class LeituraDB
{
    public async Task<List<T>> CarregarAsync<T>() where T : class
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

            var propriedade = typeof(TabelasDB).GetProperties().FirstOrDefault(p => p.PropertyType == typeof(List<T>));

            if (propriedade != null)
            {
                var valor = propriedade.GetValue(resultado) as List<T>;
                return valor ?? new List<T>();
            }

            return new List<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
            return new List<T>();
        }
    }
}
