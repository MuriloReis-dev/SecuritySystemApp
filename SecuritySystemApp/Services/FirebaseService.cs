using System.Collections.ObjectModel;
using System.ComponentModel;
using SecuritySystemApp.Services;
using SecuritySystemApp.Models;
using Plugin.Firebase.CloudMessaging;

namespace SecuritySystemApp.Services;

public class FirebaseService
{
    public async Task<string?> GetTokenAsync()
    {
        try
        {
            var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
            Console.WriteLine($"Token FMC: {token}");

            return token;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter o token do Firebase: {ex}");
            return null;
        }
    }
    
}