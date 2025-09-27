using SecuritySystemApp.ViewModels;
using SecuritySystemApp.Models;

namespace SecuritySystemApp.Views;

public partial class CadastroPessoaPage : ContentPage
{
    private readonly CadastroPessoaViewModel _viewModel;

    private List<AlarmeDTO> _selecionados = new List<AlarmeDTO>();

    public CadastroPessoaPage()
    {
        InitializeComponent();

        _viewModel = new CadastroPessoaViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AlarmesList.ItemsSource = await _viewModel.CarregarAlarmesAsync();
    }
    
    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkbox = (CheckBox)sender;
        var alarme = (AlarmeDTO)checkbox.BindingContext;

        if (e.Value) // se marcado
        {
            if (!_selecionados.Contains(alarme))
                _selecionados.Add(alarme);
        }
        else // se desmarcado
        {
            if (_selecionados.Contains(alarme))
                _selecionados.Remove(alarme);
        }
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        string nome = NomeEntry.Text?.Trim() ?? "";
        string senha = SenhaEntry.Text.Trim();

        // validações
        if (string.IsNullOrWhiteSpace(nome))
        {
            await Shell.Current.DisplayAlert("Erro", "Informe o nome", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(senha) || senha.Length != 5 || !senha.All(c => "0123456789".Contains(c)))
        {
            await Shell.Current.DisplayAlert("Erro", "A senha deve ter 5 dígitos (0 a 9)", "OK");
            return;
        }

        if (!_selecionados.Any())
        {
            await Shell.Current.DisplayAlert("Erro", "Selecione pelo menos uma porta", "OK");
            return;
        }

        await _viewModel.CadastrarAsync(nome, senha, _selecionados);
        await Shell.Current.DisplayAlert("Sucesso", $"Usuário {nome} cadastrado com sucesso!", "OK");
    }
}