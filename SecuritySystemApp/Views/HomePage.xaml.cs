using SecuritySystemApp.Services;
using SecuritySystemApp.Models;
using SecuritySystemApp.Interfaces;
namespace SecuritySystemApp.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    // Método para carregar dados do arquivo JSON ao carregar a MainPage
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var service = new LeituraDBService();
        var dados = await service.CarregarAsync<Alarme>();
        LeiturasList.ItemsSource = dados;
    }
}
