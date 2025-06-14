namespace SecuritySystemApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        CadrastroBtn.Clicked += OnCadrastroBtnClicked;
        LoginBtn.Clicked += OnLoginBtnClicked;
        HomeBtn.Clicked += OnHomeBtnClicked;

        // Switch para trocar tema (Claro ou Escuro) *MUDAR PARA A PÁGINA DE CONFIGURAÇÃO*
        if (Application.Current != null)
        {
            ThemeSwitch.IsToggled = Application.Current.UserAppTheme == AppTheme.Dark;
        }
    }
    private async void OnCadrastroBtnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CadastroPage));
    }

    private async void OnLoginBtnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

    private async void OnHomeBtnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HomePage));
    }

    // Switch para trocar tema (Claro ou Escuro) *MUDAR PARA A PÁGINA DE CONFIGURAÇÃO*
    private void OnThemeToggled(object sender, ToggledEventArgs e)
    {
        if (Application.Current != null)
        {
            Application.Current.UserAppTheme = e.Value ? AppTheme.Light : AppTheme.Dark;
        }
    }
}
