namespace SecuritySystemApp.Views;
public partial class ConfigPage : ContentPage
{
    public ConfigPage()
    {
        InitializeComponent();
    }
    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        string nome = NomeEntry.Text;
        string email = EmailEntry.Text;
        string senha = SenhaEntry.Text;

        await DisplayAlert("Sucesso", "Configurações salvas com sucesso!", "OK");
    }
}
