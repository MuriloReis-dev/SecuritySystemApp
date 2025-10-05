using SecuritySystemApp.Services;
using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;
using SecuritySystemApp.Interfaces;

namespace SecuritySystemApp.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;
    private readonly INavigationService _navigationService;

    public List<AlarmeDTO>? Dados;
    public HomePage()
    {
        InitializeComponent();
        _viewModel = new HomeViewModel();
        BindingContext = _viewModel;

        _navigationService = new NavigationService();
    }

    /// <summary>
    /// Carrega os dados dos alarmes ao aparecer a página
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Dados = await _viewModel.CarregarAlarmesAsync();
        AlarmesList.ItemsSource = Dados;

        // Ajusta visibilidade dos elementos com base nos dados carregados
        if (Dados == null || Dados.Count == 0)
            AlarmesList.IsVisible = false;
        else
            ListaVaziaLabel.IsVisible = false;
    }

    /// <summary>
    /// Evento ao tocar em um alarme para ver detalhes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnAlarmeTapped(object sender, TappedEventArgs e)
    {
        int alarmeId = e.Parameter == null ? 0 : (int)e.Parameter;
        if (alarmeId != 0)
        {
            await _navigationService.NavegarAsync(nameof(AlarmePage), new Dictionary<string, object>
            {
                ["AlarmeId"] = alarmeId
            });
        }
    }
}
