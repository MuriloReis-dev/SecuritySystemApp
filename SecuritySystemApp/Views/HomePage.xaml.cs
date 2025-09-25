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
        var alarmeSelecionado = e.CurrentSelection.FirstOrDefault() as AlarmeDTO;
        if (alarmeSelecionado != null)
        {
            await _navigationService.NavegarAsync(nameof(AlarmePage), new Dictionary<string, object>
            {
                ["AlarmeId"] = alarmeSelecionado.Id_Alarme
            });
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
