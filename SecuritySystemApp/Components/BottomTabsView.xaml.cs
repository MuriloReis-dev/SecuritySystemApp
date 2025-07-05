using SecuritySystemApp.Views;
using SecuritySystemApp.Services;
using SecuritySystemApp.Interfaces;

namespace SecuritySystemApp.Components;

public partial class BottomTabsView : ContentView
{
    private readonly INavigationService _navigationService;
    public BottomTabsView()
    {
        InitializeComponent();
        _navigationService = new NavigationService();
    }

    private async void GoToOutra(object sender, EventArgs e)
    {
        // Configurar rota para alguma página
        await _navigationService.NavegarParaAsync("nameof(Outra)");
    }

    private async void GoToHome(object sender, EventArgs e)
    {
        await _navigationService.NavegarParaAsync(nameof(HomePage));
    }

    private async void GoToConfig(object sender, EventArgs e)
    {
        await _navigationService.NavegarParaAsync(nameof(ConfigPage));
    }
}
