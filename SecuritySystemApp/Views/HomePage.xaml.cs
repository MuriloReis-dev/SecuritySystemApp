using SecuritySystemApp.Services;
using SecuritySystemApp.Models;
using SecuritySystemApp.ViewModels;
using SecuritySystemApp.Interfaces;

namespace SecuritySystemApp.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;
    private readonly INavigationService _navigationService;
    public HomePage()
    {
        InitializeComponent();
        _viewModel = new HomeViewModel();
        BindingContext = _viewModel;

        _navigationService = new NavigationService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AlarmesList.ItemsSource = await _viewModel.CarregarAlarmesAsync();
    }

    private async void OnAlarmeSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is AlarmeDTO alarmeSelecionado)
        {
            await _navigationService.NavegarAsync(nameof(AlarmePage), new Dictionary<string, object>
            {
                ["Alarme"] = alarmeSelecionado
            });
            // Opcional: desmarcar o item após o clique
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
