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

    /// <summary>
    /// Carrega os dados dos alarmes ao aparecer a página
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AlarmesList.ItemsSource = await _viewModel.CarregarAlarmesAsync();
    }
    
    /// <summary>
    /// Evento ao marcar/desmarcar um alarme
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Evento ao clicar no botão de salvar o cadastro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        string nome = NomeEntry.Text?.Trim() ?? "";
        string email = EmailEntry.Text?.Trim() ?? "";
        string senha = SenhaEntry.Text.Trim();

        // validações
        if (string.IsNullOrWhiteSpace(nome))
        {
            await Shell.Current.DisplayAlert("Erro", "Informe o nome", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            await Shell.Current.DisplayAlert("Erro", "Informe o email", "OK");
            return;
        }

        if (!_selecionados.Any())
        {
            await Shell.Current.DisplayAlert("Erro", "Selecione pelo menos um alarme", "OK");
            return;
        }

        bool sucesso = await _viewModel.CadastrarAsync(nome, email, senha, _selecionados);

        if (!sucesso)
        {
            await Shell.Current.DisplayAlert("Erro", "Falha ao cadastrar usuário.", "OK");
            return;
        }
        await Shell.Current.DisplayAlert("Sucesso", $"Usuário {nome} cadastrado com sucesso!", "OK");
    }
}