namespace SecuritySystemApp.Models;

public class AlarmeDTO
{
    public string? Id { get; set; }
    public string? Nome { get; set; }
    public bool Ligado { get; set; }
    public DateTime? DataHora { get; set; }
    public bool Adm { get; set; } // Novo campo para indicar se o alarme é do administrador
}