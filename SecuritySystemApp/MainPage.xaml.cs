using SecuritySystemApp.AppTestes;

namespace SecuritySystemApp;

public partial class MainPage : ContentPage
{
    // No futuro, mudar a MainPage para uma tela de carregamento e alterar o código para redirecionar para a tela de Login (ou para Home caso esteja logado)
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

    // Método para carregar dados do arquivo JSON ao carregar a MainPage
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var service = new LeituraDB();
        var dados = await service.CarregarAsync();
        LeiturasList.ItemsSource = dados;
    }
}
