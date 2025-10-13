namespace SecuritySystemApp.Models;

public class CadastroAddAcessoDTO
{
    // IMPORTANTE: Os nomes das propriedades devem corresponder exatamente aos nomes das colunas recebidas da requisição no banco (usar 'as' nas querys para padronizar)
    public CadastroDTO? Usuario { get; set; }
    public List<int>? Alarmes { get; set; }
}