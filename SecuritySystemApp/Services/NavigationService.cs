using SecuritySystemApp.Interfaces;

namespace SecuritySystemApp.Services;

public class NavigationService : INavigationService
{
    public async Task NavegarAsync(string pageName)
    {
        await Shell.Current.GoToAsync(pageName, animate: false);
    }

    public async Task NavegarAsync(string pageName, Dictionary<string, object> parameters)
    {
        await Shell.Current.GoToAsync(new ShellNavigationState(pageName), animate: false, parameters);
    }

    public async Task NavegarResetAsync(string pageName)
    {
        await Shell.Current.GoToAsync($"//{pageName}", animate: false);
    }

    public async Task VoltarAsync()
    {
        await Shell.Current.GoToAsync("..", animate: false);
    }
}