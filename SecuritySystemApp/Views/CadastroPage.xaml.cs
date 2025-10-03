using System.Text.RegularExpressions;
using SecuritySystemApp.ViewModels;
using SecuritySystemApp.Services;

namespace SecuritySystemApp.Views;

public partial class CadastroPage : ContentPage
{
    private readonly CadastroViewModel _viewModel;
    private readonly NavigationService _navigationService;
    public CadastroPage()
    {
        InitializeComponent();

        // Definição da ViewModel
        _viewModel = new CadastroViewModel();

        // Definição de Serviços
        _navigationService = new NavigationService();
    }

    /// <summary>
    /// Evento ao clicar no botão de enviar o cadastro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnEnviarClicked(object? sender, EventArgs e)
    {
        string nome = NomeEntry.Text?.Trim() ?? "";
        string email = EmailEntry.Text?.Trim() ?? "";
        string captcha = CaptchaEntry.Text?.Trim() ?? "";
        string senha = SenhaEntry.Text.Trim();
        string confirmarSenha = ConfirmarSenhaEntry.Text?.Trim() ?? "";

        bool emailValido = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        bool captchaValido = captcha == "7";
        bool senhavalida = senha == confirmarSenha;

        EmailErrorLabel.IsVisible = !emailValido;
        CaptchaErrorLabel.IsVisible = !captchaValido;
        SenhaErrorLabel.IsVisible = !senhavalida;

        if (emailValido && captchaValido && senhavalida)
        {
            //realiza cadastro
            var sucesso = await _viewModel.Cadastrar(nome, email, senha);
            if (!sucesso)
            {
                await DisplayAlert("Erro", "O usuário não foi cadastrado", "OK");
            }
            else
            {
                await _navigationService.NavegarResetAsync("HomePageReset"); // Navega para a HomePage e reseta a pilha de navegação
            }
        }
    }

    /// <summary>
    /// Evento ao clicar no botão de voltar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnVoltarClicked(object? sender, EventArgs e)
    {
        await _navigationService.VoltarAsync(); // volta uma página no histórico
    }
}
