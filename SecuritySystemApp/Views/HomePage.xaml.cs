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
