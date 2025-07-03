using SecuritySystemApp.Views;

namespace SecuritySystemApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(CadastroPage), typeof(CadastroPage));
		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
		Routing.RegisterRoute(nameof(ConfigPage), typeof(ConfigPage));
	}
}
