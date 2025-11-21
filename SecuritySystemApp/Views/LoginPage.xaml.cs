using System.Text.RegularExpressions;
using SecuritySystemApp.ViewModels;
using SecuritySystemApp.Services;

namespace SecuritySystemApp.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _viewModel;
    private readonly NavigationService _navigationService;
    public LoginPage()
    {
        InitializeComponent();

        // Definição da ViewModel
        _viewModel = new LoginViewModel();

        // Definição de Serviços
        _navigationService = new NavigationService();
    }

    /// <summary>
    /// Evento ao clicar no botão de enviar o login
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnEnviarClicked(object? sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim() ?? "";
        string captcha = CaptchaEntry.Text?.Trim() ?? "";
        string senha = SenhaEntry.Text?.Trim() ?? "";

        // Valida o formato dos inputs
        bool emailValido = email != "" && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        bool captchaValido = captcha != "" && captcha == "7";
        bool senhaValida = senha != "" && senha.Length >= 8;

        EmailErrorLabel.IsVisible = !emailValido;
        CaptchaErrorLabel.IsVisible = !captchaValido;
        SenhaErrorLabel.IsVisible = !senhaValida;

        if (emailValido && captchaValido && senhaValida)
        {
            var sucesso = await _viewModel.Entrar(email, senha);
            if (sucesso)
            {
                await _navigationService.NavegarResetAsync("HomePageReset"); // Navega para a HomePage e reseta a pilha de navegação
                EmailEntry.Text = "";
                CaptchaEntry.Text = "";
                SenhaEntry.Text = "";
            }
            else
            {
                await DisplayAlert("Erro", "Email ou Senha incorretos", "OK");
            }
        }
        SenhaEntry.Text = "";
    }

    /// <summary>
    /// Evento ao clicar no link de cadastro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnCadastroClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(CadastroPage)); // Vai para a página de cadastro
    }
}
