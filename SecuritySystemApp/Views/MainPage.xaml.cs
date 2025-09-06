using SecuritySystemApp.Services;
using SecuritySystemApp.Interfaces;
using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;

namespace SecuritySystemApp.Views;

public partial class MainPage : ContentPage
{
    // No futuro, mudar a MainPage para uma tela de carregamento e alterar o código para redirecionar para a tela de Login (ou para Home caso esteja logado)
    private readonly MainViewModel _viewModel;

    private readonly INavigationService _navigationService;
    private readonly ApiService _apiService;
    private readonly AuthService _authService;

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        _navigationService = new NavigationService();
        _apiService = new ApiService();
        _authService = new AuthService();

        CadrastroBtn.Clicked += OnCadrastroBtnClicked;
        LoginBtn.Clicked += OnLoginBtnClicked;
        HomeBtn.Clicked += OnHomeBtnClicked;
        Appearing += OnAppearing;
        ThemeSwitch.Toggled += OnThemeToggled;

        // Switch para trocar tema (Claro ou Escuro) *MUDAR PARA A PÁGINA DE CONFIGURAÇÃO*
        if (Application.Current != null)
        {
            ThemeSwitch.IsToggled = Application.Current.UserAppTheme == AppTheme.Dark;
        }
    }

    // Evento disparado quando a página aparece
    private async void OnAppearing(object? sender, EventArgs e)
    {
        base.OnAppearing();

        // Colocar aqui todo o código executado ao abrir o app
        Console.WriteLine($"UserID: {Preferences.Get("UserId", string.Empty)}");
        Console.WriteLine($"UserName: {Preferences.Get("UserName", string.Empty)}");
        Console.WriteLine($"UserEmail: {Preferences.Get("UserEmail", string.Empty)}");
        Console.WriteLine($"AuthToken: {Preferences.Get("AuthToken", string.Empty)}");
        
        bool tokenValido = await _authService.ValidateLoginAsync();
        Console.WriteLine($"Validação do token de usuário: {tokenValido}");
    }

    private async void OnCadrastroBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarParaAsync(nameof(CadastroPage));
    }

    private async void OnLoginBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarParaAsync(nameof(LoginPage));
    }

    private async void OnHomeBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarParaAsync(nameof(HomePage));
    }

    // Switch para trocar tema (Claro ou Escuro) *MUDAR PARA A PÁGINA DE CONFIGURAÇÃO*
    private void OnThemeToggled(object? sender, ToggledEventArgs e)
    {
        if (Application.Current != null)
        {
            Application.Current.UserAppTheme = e.Value ? AppTheme.Light : AppTheme.Dark;
        }
    }
}
