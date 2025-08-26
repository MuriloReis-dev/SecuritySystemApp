using System.Text.RegularExpressions;
namespace SecuritySystemApp.Views;
using SecuritySystemApp.ViewModels;

public partial class LoginPage : ContentPage
{
    LoginViewModel ViewModel;
    public LoginPage()
    {
        InitializeComponent();
        ViewModel = new LoginViewModel();
    }

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
            var (_, sucesso) = await ViewModel.Entrar(email, senha);
            if (sucesso)
            {
                await DisplayAlert("Sucesso", "Login enviado com sucesso!", "OK");
            }
        }
    }

    private async void OnVoltarClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(".."); // volta uma página no histórico
    }
}
