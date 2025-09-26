namespace SecuritySystemApp.Models;

public class Pessoa
{
    public string Nome { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public List<int> PortasIds { get; set; } = new();
}