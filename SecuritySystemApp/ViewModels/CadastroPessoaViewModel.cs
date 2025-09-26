using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SecuritySystemApp.Models;
using System;

namespace SecuritySystemApp.ViewModels;

public partial class CadastroPessoaViewModel : ObservableObject
{
    // Inicializa para evitar warnings de nullability
    [ObservableProperty]
    private string nome = string.Empty;

    [ObservableProperty]
    private string senha = string.Empty;

    // Lista de portas para mostrar na UI
    public ObservableCollection<PortaViewModel> Portas { get; } = new ObservableCollection<PortaViewModel>
    {
        new PortaViewModel { Id = 1, Nome = "Porta 1" },
        new PortaViewModel { Id = 2, Nome = "Porta 2" },
        new PortaViewModel { Id = 3, Nome = "Porta 3" },
        new PortaViewModel { Id = 4, Nome = "Porta 4" },
        new PortaViewModel { Id = 5, Nome = "Porta 5" }
    };

    // Comando gerado pelo [RelayCommand] — método assíncrono que cria a pessoa
    [RelayCommand]
    private async Task CadastrarAsync()
    {
        // validações
        if (string.IsNullOrWhiteSpace(Nome))
        {
            await Shell.Current.DisplayAlert("Erro", "Informe o nome", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(Senha) || Senha.Length != 5 || !Senha.All(c => "123456789".Contains(c)))
        {
            await Shell.Current.DisplayAlert("Erro", "A senha deve ter 5 dígitos (1 a 9)", "OK");
            return;
        }

        var selecionadas = Portas.Where(p => p.IsSelecionada).ToList();
        if (!selecionadas.Any())
        {
            await Shell.Current.DisplayAlert("Erro", "Selecione pelo menos uma porta", "OK");
            return;
        }

        // criar objeto pessoa (aqui só em memória; salve em DB/API se quiser)
        var pessoa = new Pessoa
        {
            Nome = Nome,
            Senha = Senha,
            PortasIds = selecionadas.Select(p => p.Id).ToList()
        };

        // *AQUI É ONDE COLOQUEI O DisplayAlert* — após criar a pessoa
        await Shell.Current.DisplayAlert("Sucesso", $"Pessoa {pessoa.Nome} cadastrada com sucesso!", "OK");

        // resetar formulário
        Nome = string.Empty;
        Senha = string.Empty;
        foreach (var p in Portas) p.IsSelecionada = false;
    }
}

public partial class PortaViewModel : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty]
    private string nome = string.Empty;

    [ObservableProperty]
    private bool isSelecionada;
}