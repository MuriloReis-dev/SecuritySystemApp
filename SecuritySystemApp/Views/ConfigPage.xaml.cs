using SecuritySystemApp.Services;
using SecuritySystemApp.Interfaces;
using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;

namespace SecuritySystemApp.Views;

public partial class ConfigPage : ContentPage
{

    private readonly INavigationService _navigationService;
    private readonly ApiService _apiService;
    private readonly AuthService _authService;
    public ConfigPage()
    {
        InitializeComponent();

        // Serviços
        _navigationService = new NavigationService();
        _apiService = new ApiService();
        _authService = new AuthService();

        // Eventos
        SaveProfileButton.Clicked += OnSaveProfileButtonClicked;
        ThemeSwitch.Toggled += OnThemeToggled;
    }
    private async void OnSaveProfileButtonClicked(object? sender, EventArgs e)
    {
        string senhaAtual = SenhaAtualEntry.Text;
        string novaSenha = NovaSenhaEntry.Text;

        await DisplayAlert("Sucesso", "Configurações salvas com sucesso!", "OK");
    }

    private void OnThemeToggled(object? sender, ToggledEventArgs e)
    {
        if (Application.Current != null)
        {
            Application.Current.UserAppTheme = e.Value ? AppTheme.Light : AppTheme.Dark;
        }
    }
}
