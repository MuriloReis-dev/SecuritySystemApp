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
    }

    /// <summary>
    /// Evento ao clicar no botão de salvar as configurações do perfil
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnSaveProfileButtonClicked(object? sender, EventArgs e)
    {
        string senhaAtual = SenhaAtualEntry.Text;
        string novaSenha = NovaSenhaEntry.Text;

        await DisplayAlert("Sucesso", "Configurações salvas com sucesso!", "OK");
    }

    /// <summary>
    /// Evento ao tocar na área de tema para escolher o tema do aplicativo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnThemeAreaTapped(object? sender, EventArgs e)
    {
        string action = await DisplayActionSheet(
            "Escolha o tema",
            "Cancelar",
            null,
            "Sistema",
            "Claro",
            "Escuro"
        );

        if (Application.Current != null)
        {
            switch (action)
            {
                case "Sistema":
                    Application.Current.UserAppTheme = AppTheme.Unspecified;
                    break;
                case "Claro":
                    Application.Current.UserAppTheme = AppTheme.Light;
                    break;
                case "Escuro":
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    break;

            }
        }
    }
}
