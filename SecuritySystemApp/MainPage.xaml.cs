namespace SecuritySystemApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        CadrastroBtn.Clicked += OnCadrastroBtnClicked;
    }
    private async void OnCadrastroBtnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CadastroPage));
    }

    // Adicionar código da rota para tela de Login
    
}
