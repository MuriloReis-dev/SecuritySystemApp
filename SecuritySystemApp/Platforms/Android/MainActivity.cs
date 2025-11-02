using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using System;
using Firebase;

namespace SecuritySystemApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        try
        {
            // Inicializa Firebase usando a Activity (Context válido)
            FirebaseApp.InitializeApp(this);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao inicializar FirebaseApp: {ex}");
        }

        // Se o plugin de push que você usa requer inicialização manual, chame aqui.
        // Exemplo (se disponível): FirebasePushNotificationManager.Initialize(this, true);
    }

    protected override void OnNewIntent(Intent? intent)
    {
        base.OnNewIntent(intent);

        try
        {
            if (intent is not null)
            {
                // Encaminhar intent para o plugin de push, se o plugin expõe um método para isso.
                // Exemplo (se disponível): FirebasePushNotificationManager.ProcessIntent(this, intent);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar OnNewIntent: {ex}");
        }
    }
}
