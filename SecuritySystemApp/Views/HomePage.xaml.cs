using SecuritySystemApp.Services;
using SecuritySystemApp.ViewModels;
using SecuritySystemApp.Interfaces;
namespace SecuritySystemApp.Views;

public partial class HomePage : ContentPage
{
    HomeViewModel ViewModel;
    public HomePage()
    {
        InitializeComponent();
        ViewModel = new HomeViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AlarmesList.ItemsSource = await ViewModel.CarregarAlarmesAsync();
    }
}
