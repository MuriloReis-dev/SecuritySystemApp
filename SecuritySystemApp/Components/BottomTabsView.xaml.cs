using SecuritySystemApp.Views;
using SecuritySystemApp.Services;
using SecuritySystemApp.Interfaces;

namespace SecuritySystemApp.Components;

public enum BottomTabPage
{
    NotifyPage,
    HomePage,
    ConfigPage
}

public partial class BottomTabsView : ContentView
{
    private readonly INavigationService _navigationService;

    public static readonly BindableProperty CurrentPageProperty =
        BindableProperty.Create(
            nameof(CurrentPage),
            typeof(BottomTabPage),
            typeof(BottomTabsView),
            BottomTabPage.HomePage,
            propertyChanged: OnCurrentPageChanged);

    public BottomTabPage CurrentPage
    {
        get => (BottomTabPage)GetValue(CurrentPageProperty);
        set => SetValue(CurrentPageProperty, value);
    }

    public BottomTabsView()
    {
        InitializeComponent();
        UpdateIcons();

        // Definição dos Serviços
        _navigationService = new NavigationService();

        // Atualiza ícones quando o tema do app mudar
        if (Application.Current != null)
            Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
    }

    /// <summary>
    /// Evento de clique no botão de notificações
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnNotifyClicked(object sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(NotifyPage));
    }

    /// <summary>
    /// Evento de clique no botão de Home
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(HomePage));
    }

    /// <summary>
    /// Evento de clique no botão de Configurações
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnConfigClicked(object sender, EventArgs e)
    {
        await _navigationService.NavegarAsync(nameof(ConfigPage));
    }

    /// <summary>
    /// Atualiza os ícones com base na página atual
    /// </summary>
    /// <param name="bindable"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    private static void OnCurrentPageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (BottomTabsView)bindable;
        control.UpdateIcons();
    }

    /// <summary>
    /// Atualiza os ícones quando o tema do app mudar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        UpdateIcons();
    }

    /// <summary>
    /// Atualiza os ícones com base na página atual e tema
    /// </summary>
    private void UpdateIcons()
    {
        if (Application.Current?.RequestedTheme == AppTheme.Dark)
        {
            if (CurrentPage == BottomTabPage.NotifyPage)
                NotifyButton.ImageSource = "notify_white_full.png";
            else if (CurrentPage == BottomTabPage.HomePage)
                HomeButton.ImageSource = "homeicon.png";
            else if (CurrentPage == BottomTabPage.ConfigPage)
                ConfigButton.ImageSource = "config_white_full.png";
        }
        else
        {
            if (CurrentPage == BottomTabPage.NotifyPage)
                NotifyButton.ImageSource = "notify_black_full.png";
            else if (CurrentPage == BottomTabPage.HomePage)
                HomeButton.ImageSource = "homeicon.png";
            else if (CurrentPage == BottomTabPage.ConfigPage)
                ConfigButton.ImageSource = "config_black_full.png";
        }
    }
}
