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
    }

    private async void OnNotifyClicked(object sender, EventArgs e)
    {
        await _navigationService.NavegarParaAsync(nameof(NotifyPage));
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await _navigationService.NavegarParaAsync(nameof(HomePage));
    }

    private async void OnConfigClicked(object sender, EventArgs e)
    {
        await _navigationService.NavegarParaAsync(nameof(ConfigPage));
    }

    private static void OnCurrentPageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (BottomTabsView)bindable;
        control.UpdateIcons();
    }

    private void UpdateIcons()
    {
        if (CurrentPage == BottomTabPage.NotifyPage)
        {
            if (Application.Current?.RequestedTheme == AppTheme.Dark)
                NotifyButton.ImageSource = "notify_white_full.png";
            else
                NotifyButton.ImageSource = "notify_black_full.png";
        }
        if (CurrentPage == BottomTabPage.HomePage)
        {
            HomeButton.ImageSource = "homeicon.png";
        }
        if (CurrentPage == BottomTabPage.ConfigPage)
        {
            if (Application.Current?.RequestedTheme == AppTheme.Dark)
                ConfigButton.ImageSource = "config_white_full.png";
            else
                ConfigButton.ImageSource = "config_black_full.png";
        }
    }
}
