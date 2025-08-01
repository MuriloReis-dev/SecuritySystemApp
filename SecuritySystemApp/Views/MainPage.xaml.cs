﻿using SecuritySystemApp.Services;
using SecuritySystemApp.Interfaces;

namespace SecuritySystemApp.Views;

public partial class MainPage : ContentPage
{
    // No futuro, mudar a MainPage para uma tela de carregamento e alterar o código para redirecionar para a tela de Login (ou para Home caso esteja logado)
    private readonly INavigationService _navigationService;

    public MainPage()
    {
        InitializeComponent();
        _navigationService = new NavigationService();
        
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
        await _navigationService.NavegarParaAsync(nameof(CadastroPage));
    }

    private async void OnLoginBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarParaAsync(nameof(LoginPage));
    }

    private async void OnHomeBtnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavegarParaAsync(nameof(HomePage));
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
