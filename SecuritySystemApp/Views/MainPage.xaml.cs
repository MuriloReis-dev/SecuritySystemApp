using SecuritySystemApp.Services;
using SecuritySystemApp.Interfaces;
using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;
using Plugin.FirebasePushNotifications;
using Microsoft.Maui.Networking;
using Microsoft.Maui.ApplicationModel;

namespace SecuritySystemApp.Views;

public partial class MainPage : ContentPage
{
    // No futuro, mudar a MainPage para uma tela de carregamento e alterar o código para redirecionar para a tela de Login (ou para Home caso esteja logado)
    private readonly MainViewModel _viewModel;

    private readonly INavigationService _navigationService;
    private readonly ApiService _apiService;
    private readonly AuthService _authService;
    private readonly FirebaseService _firebaseService;

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new MainViewModel();
        _navigationService = new NavigationService();
        _apiService = new ApiService();
        _authService = new AuthService();
        _firebaseService = new FirebaseService();
    }

    public async Task InitFirebaseMessagingAsync()
    {
        var token = await _firebaseService.GetTokenAsync();
        Console.WriteLine($"Firebase Token: {token}");
    }

    // Evento disparado quando a página aparece
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            // Espera um momento para garantir que a UI esteja pronta
            await Task.Delay(100);

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                LoadingLabel.IsVisible = true;
                ReloadPageArea.IsVisible = false;
            });

            var currentAccess = Connectivity.Current.NetworkAccess;
            Console.WriteLine($"Current network access: {currentAccess}");

            if (currentAccess == NetworkAccess.Internet)
            {
                Console.WriteLine("Conectado à Internet");
                bool tokenValido = await _authService.ValidateLoginAsync();
                Console.WriteLine($"Token válido: {tokenValido}");

                if (tokenValido)
                {
                    await _navigationService.NavegarResetAsync("HomePageReset");
                }
                else
                {
                    await _navigationService.NavegarResetAsync("LoginPageReset");
                    await DisplayAlert("Validação de Login Falhou", "Por favor, faça login novamente.", "OK");
                }
            }
            else
            {
                Console.WriteLine("Sem conexão com a Internet");
                await Task.Delay(100); // Pequeno delay para garantir que a UI está pronta
                await DisplayAlert("Sem Conexão", "Erro ao Conectar", "OK");
                
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    LoadingLabel.IsVisible = false;
                    ReloadPageArea.IsVisible = true;
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro em MainPage OnAppearing: {ex}");
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                LoadingLabel.IsVisible = false;
                ReloadPageArea.IsVisible = true;
            });
        }
    }

    public void OnReloadBtnClicked(object? sender, EventArgs e)
    {
        OnAppearing();
    }

    private async void OnCadrastroBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(CadastroPage));
    }

    private async void OnLoginBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(LoginPage));
    }

    private async void OnHomeBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(HomePage));
    }

    private async void OnCadastroPessoaBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(CadastroPessoaPage));
    }
}
