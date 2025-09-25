namespace SecuritySystemApp.Models;

public class AlarmeDTO
{
    public int Id_Alarme { get; set; }
    public string? Nome { get; set; }
    public bool Ligado { get; set; }
    public DateTime? DataHora { get; set; }
    public string? Tipo_Acesso { get; set; } // Novo campo para indicar se o alarme é do administrador
}