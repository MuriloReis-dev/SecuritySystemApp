using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;
using Plugin.FirebasePushNotifications;

namespace SecuritySystemApp.Services;

public class FirebaseService
{
    public async Task<string?> GetTokenAsync()
    {
        try
        {
            // Usa o token provido pelo plugin cliente (consistente com MainViewModel)
            var token = IFirebasePushNotification.Current?.Token;
            Console.WriteLine($"Token FMC: {token}");
            return await Task.FromResult(token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter o token do Firebase: {ex}");
            return null;
        }
    }
    
}