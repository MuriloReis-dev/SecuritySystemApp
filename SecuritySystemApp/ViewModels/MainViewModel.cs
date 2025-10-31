using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;
using Plugin.FirebasePushNotifications;
using Microsoft.Extensions.Logging;


namespace SecuritySystemApp.ViewModels;

public class MainViewModel
{
    private readonly ApiService _apiService;

    public string Token { get; set; }

    public MainViewModel()
    {
        // Definição dos Serviços
        _apiService = new ApiService();
        Token = "?";

        // 🔹 Pede permissão para receber notificações
        RequestNotificationPermission();

        // 🔹 Registra o app para receber notificações e obtém o token
        RegisterForPushNotifications();
    }

    private async void RequestNotificationPermission()
    {
        try
        {
            await INotificationPermissions.Current.RequestPermissionAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao pedir permissão: {ex.Message}");
        }
    }

    private async void RegisterForPushNotifications()
    {
        try
        {
            await IFirebasePushNotification.Current.RegisterForPushNotificationsAsync();
            Token = IFirebasePushNotification.Current.Token;
            Console.WriteLine($"Token: {Token}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao registrar FCM: {ex.Message}");
            Token = $"Erro: {ex.Message}";
        }
    }
}