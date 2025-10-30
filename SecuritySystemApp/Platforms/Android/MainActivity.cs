using Android.App;
using Android.Content.PM;
using Android.OS;
using Firebase;

namespace SecuritySystemApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu) // Android 13 (API 33)
        {
            // Solicita permissão para notificações em Android 13 ou superior
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M) // Android 6.0 (API 23)
            {
                if(CheckSelfPermission(Android.Manifest.Permission.PostNotifications) != Permission.Granted)
                {
                    RequestPermissions(new[] { Android.Manifest.Permission.PostNotifications }, 0);
                }
            }
        }

        // Inicializa o Firebase
        try
        {
            FirebaseApp.InitializeApp(this);
            Console.WriteLine("Firebase inicializado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao inicializar o Firebase: {ex.Message}");
        }
    }
}
