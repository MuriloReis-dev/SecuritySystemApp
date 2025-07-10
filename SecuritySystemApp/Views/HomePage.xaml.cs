using SecuritySystemApp.Services;
using SecuritySystemApp.Models;
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
        BindingContext = ViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.CarregarAlarmesAsync();
    }
}
