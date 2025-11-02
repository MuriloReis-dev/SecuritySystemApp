using Plugin.FirebasePushNotifications;
using SecuritySystemApp.Services;

namespace SecuritySystemApp;

public partial class App : Application
{
	private readonly FirebaseService _firebaseService = new FirebaseService();
	public App()
	{
		InitializeComponent();

		try
		{
			IFirebasePushNotification.Current.TokenRefreshed += async (s, p) =>
			{
				Console.WriteLine($"Token FMC atualizado: {p.Token}");
				await _firebaseService.UpdateTokenAsync(p.Token);
			};
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro ao configurar notificações push do Firebase: {ex}");
		}
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}