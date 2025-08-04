namespace SecuritySystemApp.Models;

public class Alarme
{
    public string? Id { get; set; }
    public string? Nome { get; set; }
    public bool Ligado { get; set; }
    public DateTime? DataHora { get; set; }
}