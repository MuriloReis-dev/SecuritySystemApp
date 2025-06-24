namespace SecuritySystemApp.Components;

public partial class BottomTabsView : ContentView
{
    public BottomTabsView()
    {
        InitializeComponent();
    }

    private async void GoToOutra(object sender, EventArgs e)
    {
        // Configurar rota para alguma página
        await Shell.Current.GoToAsync("nameof(Outra)");
    }

    private async void GoToHome(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HomePage));
    }

    private async void GoToConfig(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ConfigPage));
    }
}
