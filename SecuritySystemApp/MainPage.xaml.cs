namespace SecuritySystemApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        CadrastroBtn.Clicked += OnCadrastroBtnClicked;
        LoginBtn.Clicked += OnLoginBtnClicked;
    }
    private async void OnCadrastroBtnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CadastroPage));
    }

    private async void OnLoginBtnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}
