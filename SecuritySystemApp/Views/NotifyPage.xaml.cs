using SecuritySystemApp.Services;
using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;
using SecuritySystemApp.Interfaces;

namespace SecuritySystemApp.Views;

public partial class NotifyPage : ContentPage
{
    private readonly NotifyViewModel _viewModel;
    private readonly INavigationService _navigationService;

    public List<NotifyDTO>? Dados;
    public NotifyPage()
    {
        InitializeComponent();
        _viewModel = new NotifyViewModel();
        BindingContext = _viewModel;

        _navigationService = new NavigationService();
    }

    /// <summary>
    /// Carrega os dados dos alarmes ao aparecer a página
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Dados = await _viewModel.CarregarNotificacoesAsync();
        NotifyList.ItemsSource = Dados;

        // Ajusta visibilidade dos elementos com base nos dados carregados
        if (Dados == null || Dados.Count == 0)
            NotifyList.IsVisible = false;
        else
            ListaVaziaLabel.IsVisible = false;
    }
}
