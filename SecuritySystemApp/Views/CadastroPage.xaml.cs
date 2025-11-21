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
        string senha = SenhaEntry.Text?.Trim() ?? "";
        string confirmarSenha = ConfirmarSenhaEntry.Text?.Trim() ?? "";

        bool nomeValido = nome != "" && !Regex.IsMatch(nome, @"[^\p{L}\p{N}]");
        bool emailValido = email != "" && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        bool captchaValido = captcha != "" && captcha == "7";
        bool senhaValida = senha != "" && senha.Length >= 8;
        bool confirmarSenhaValida = senha == confirmarSenha;

        NomeErrorLabel.IsVisible = !nomeValido;
        EmailErrorLabel.IsVisible = !emailValido;
        CaptchaErrorLabel.IsVisible = !captchaValido;
        SenhaErrorLabel.IsVisible = !senhaValida;
        ConfirmarSenhaErrorLabel.IsVisible = !confirmarSenhaValida;

        if (nomeValido && emailValido && captchaValido && senhaValida && confirmarSenhaValida)
        {
            //realiza cadastro
            var sucesso = await _viewModel.Cadastrar(nome, email, senha);
            if (sucesso)
            {
                await _navigationService.NavegarResetAsync("HomePageReset"); // Navega para a HomePage e reseta a pilha de navegação
                NomeEntry.Text = "";
                EmailEntry.Text = "";
                CaptchaEntry.Text = "";
                SenhaEntry.Text = "";
                ConfirmarSenhaEntry.Text = "";
            }
            else
            {
                await DisplayAlert("Erro", "O usuário não foi cadastrado", "OK");
            }
        }
        SenhaEntry.Text = "";
        ConfirmarSenhaEntry.Text = "";
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
