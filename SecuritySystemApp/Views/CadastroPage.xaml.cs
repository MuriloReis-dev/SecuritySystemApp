using System.Text.RegularExpressions;
namespace SecuritySystemApp.Views;
using SecuritySystemApp.ViewModels;

public partial class CadastroPage : ContentPage
{
    CadastroViewModel ViewModel;
    public CadastroPage()
    {
        InitializeComponent();
        ViewModel = new CadastroViewModel();
    }

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
            var sucesso = await ViewModel.Cadastrar(nome, email, senha);
            if (sucesso)
            {
                await DisplayAlert("Sucesso", "Cadastro enviado com sucesso!", "OK");
            }
        }
    }

    private async void OnVoltarClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(".."); // volta uma página no histórico
    }
}
