using System.Text.RegularExpressions;
namespace SecuritySystemApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void OnEnviarClicked(object? sender, EventArgs e)
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
            //add validação de login
        }
    }

    private async void OnVoltarClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(".."); // volta uma página no histórico
    }
}
