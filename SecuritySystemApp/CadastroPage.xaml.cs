using System.Text.RegularExpressions;
namespace SecuritySystemApp;

public partial class CadastroPage : ContentPage
{
    public CadastroPage()
    {
        InitializeComponent();
    }

    private async void OnEnviarClicked(object? sender, EventArgs e)
    {
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
            await DisplayAlert("Sucesso", "Cadastro enviado com sucesso!", "OK");
            // Adicionar sistema de cadastro
            
        }
    }

    private async void OnVoltarClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(".."); // volta uma página no histórico
    }
}
