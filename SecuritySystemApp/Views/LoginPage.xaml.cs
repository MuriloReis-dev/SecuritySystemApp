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
        string senha = SenhaEntry.Text.Trim();

        bool emailValido = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        bool captchaValido = captcha == "7";

        EmailErrorLabel.IsVisible = !emailValido;
        CaptchaErrorLabel.IsVisible = !captchaValido;

        if (emailValido && captchaValido)
        {
            var sucesso = await _viewModel.Entrar(email, senha);
            if (!sucesso)
            {
                await DisplayAlert("Erro", "Email ou Senha incorretos", "OK");
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
